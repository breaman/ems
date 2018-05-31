using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EMS.Domain.Abstract;
using EMS.Domain.Models;
using EMS.Web.Models;
using EMS.Web.Services;
using EMS.Web.ViewModels;
using IntelliTect.PaymentGateway.Interfaces;
using IntelliTect.PaymentGateway.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class CheckoutController : Controller
    {
        public ITeamRepository TeamRepository { get; }
        public UserManager<User> UserManager { get; }
        public AppSettings AppSettings { get; }
        public IPaymentService PaymentService { get; }
        public ITransactionHistoryRepository TransactionHistoryRepository { get; }
        public IEmailViewRenderer EmailViewRenderer { get; }
        public IMapper Mapper { get; }
        public IEmailSender EmailSender { get; }
        public CheckoutController(ITeamRepository teamRepository, UserManager<User> userManager,
            IOptions<AppSettings> appSettings, IPaymentService paymentService,
            ITransactionHistoryRepository transactionHistoryRepository, IEmailViewRenderer emailViewRenderer,
            IMapper mapper, IEmailSender emailSender)
        {
            TeamRepository = teamRepository;
            UserManager = userManager;
            AppSettings = appSettings.Value;
            PaymentService = paymentService;
            TransactionHistoryRepository = transactionHistoryRepository;
            EmailViewRenderer = emailViewRenderer;
            Mapper = mapper;
            EmailSender = emailSender;
        }

        // GET: api/values
        [HttpGet]
        [ResponseCache(NoStore = true)]
        public async Task<CheckoutViewModel> Get()
        {
            var managerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CheckoutViewModel viewModel = new CheckoutViewModel();

            // get the teams
            var teams = await TeamRepository.All.Include(t => t.Division).Include(t => t.Members).Include(t => t.PromotionalCode).Include(t => t.Gender).Where(t => t.ManagerId == managerId).ToListAsync();
            viewModel.Teams = new List<CheckoutViewModel.TeamInfo>();
            foreach (var team in teams)
            {
                var teamInfo = new CheckoutViewModel.TeamInfo()
                {
                    TeamId = team.Id,
                    Name = team.Name,
                    Division = team.Division.Name,
                    Cost = team.Division.Cost.Value
                };

                if (team.Members != null && team.Members.Count > 0)
                {
                    teamInfo.PlayerCount = team.Members.Count;
                }

                if (team.PromotionalCode != null)
                {
                    teamInfo.PromoCode = team.PromotionalCode.PromoCode;
                    // calculate the cost
                    if (team.PromotionalCode.FlatRateCost != null)
                    {
                        teamInfo.Cost = team.PromotionalCode.FlatRateCost.Value;
                    }
                    if (team.PromotionalCode.DiscountPercentage != null)
                    {
                        var discountAmount = (team.Division.Cost.Value * (team.PromotionalCode.DiscountPercentage.Value * .01M));
                        teamInfo.Cost = (team.Division.Cost.Value - discountAmount);
                    }
                }

                if (Validate(team) && team.PaymentTransactionId == null)
                {
                    viewModel.Teams.Add(teamInfo);
                }
            }
            // get the player changes
            // get the manager information for billing info
            var user = await UserManager.FindByIdAsync(managerId.ToString());
            viewModel.CcInfo = new CheckoutViewModel.CCInfo()
            {
                Address = user.Address1,
                City = user.City,
                CountryCode = user.Country,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PostalCode = user.Zip,
                State = user.StateProvince
            };

            return viewModel;
        }

        // POST api/values
        [HttpPost]
        public async Task<CheckoutViewModel> Post([FromBody]CheckoutViewModel viewModel)
        {
            var managerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await UserManager.FindByIdAsync(managerId.ToString());
            var teamIds = viewModel.Teams.Select(t => t.TeamId).ToList();
            // get all the teams that the manager is paying for (and only their teams)
            var actualTeams = await TeamRepository.All.Where(t => teamIds.Contains(t.Id) && t.ManagerId == managerId).Include(t => t.PromotionalCode).Include(t => t.Division).ToListAsync();
            var transactionAmount = 0M;

            viewModel.Success = true;
            viewModel.Errors = new List<string>();

            if (!viewModel.AgreesToTerms)
            {
                viewModel.Success = false;
                viewModel.Errors.Add("You must accept the license agreement in order to continue.");
            }
            else
            {
                if (actualTeams.Count > 0)
                {
                    transactionAmount = CalculateTransactionAmount(actualTeams);

                    if (transactionAmount <= 0)
                    {
                        await UpdateFreeTransaction(actualTeams);
                    }
                    else
                    {
                        var cardType = CardValidator.DetermineCardType(viewModel.CcInfo.CardNumber);
                        if (cardType != CardType.AMEX && cardType != CardType.Visa
                           && cardType != CardType.MasterCard && cardType != CardType.Discover)
                        {
                           viewModel.Success = false;
                           viewModel.Errors.Add("You need to use either a Visa, Mastercard, Discover Card, or American Express for payment.");
                        }

                        if (viewModel.Success)
                        {
                            viewModel.Success = await ProcessPayment(viewModel, user, transactionAmount, actualTeams);
                            if (!viewModel.Success)
                            {
                                viewModel.Errors.Add("An error occurred while trying to process your payment. Please validate all information is correct and then try again. If this continues, please contact us for support.");
                            }
                            else
                            {
                                // Send confirmation email
                                await SendConfirmationEmail(teamIds);
                            }
                        }
                    }
                }
                else
                {
                    viewModel.Errors.Add("There are no teams that you are a manager of in your shopping cart.");
                    viewModel.Success = false;
                }
            }

            return viewModel;
        }

        private async Task<bool> ProcessPayment(CheckoutViewModel viewModel, User user, decimal transactionAmount, List<Team> teams)
        {
            bool success = true;

            var connectionInfo = new ConnectionInformation
            {
               AppKey = AppSettings.Payment.ClientId,
               AppSecret = AppSettings.Payment.ClientSecret,
               Url = AppSettings.Payment.BaseUrl
            };

            var paymentInfo = new PaymentInformation
            {
               FirstName = viewModel.CcInfo.FirstName,
               LastName = viewModel.CcInfo.LastName,
               Address1 = viewModel.CcInfo.Address,
               City = viewModel.CcInfo.City,
               State = viewModel.CcInfo.State,
               PostalCode = viewModel.CcInfo.PostalCode,
               Country = viewModel.CcInfo.CountryCode,
               PhoneNumber = user.PhoneNumber,
               EmailAddress = user.Email,
               CreditCardNumber = viewModel.CcInfo.CardNumber,
               ExpirationDate = new DateTime(viewModel.CcInfo.Year, viewModel.CcInfo.Month, 1),
               Csc = viewModel.CcInfo.Cvv,
               TransactionAmount = transactionAmount,
            };

            ITransactionResponse response = await PaymentService.ProcessPaymentAsync(connectionInfo, paymentInfo);

            if (response != null && response.IsSuccess)
            {
               foreach (Team team in teams)
               {
                   team.PaymentTransactionId = response.TransactionId;
               }
               var transaction = new TransactionHistory()
               {
                   ManagerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                   Amount = transactionAmount,
                   PaymentStatus = response.PaymentStatus,
                   TransactionDate = response.TransactionDate,
                   TransactionId = response.TransactionId
               };

               await TeamRepository.SaveAsync();
               TransactionHistoryRepository.InsertOrUpdate(transaction);
               await TransactionHistoryRepository.SaveAsync();
            }
            else
            {
               success = false;
               // need to log full response that came back
            }

            return success;
        }

        private async Task UpdateFreeTransaction(List<Team> teams)
        {
            DateTime transactionId = DateTime.Now;
            string transactionIdString = transactionId.ToString("yyyyMMddHHmmssfff");

            foreach (var team in teams)
            {
                team.PaymentTransactionId = transactionIdString;
            }
            var transaction = new TransactionHistory()
            {
                ManagerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Amount = 0,
                PaymentStatus = "Processed",
                TransactionDate = transactionId,
                TransactionId = transactionIdString
            };

            await TeamRepository.SaveAsync();
            TransactionHistoryRepository.InsertOrUpdate(transaction);
            await TransactionHistoryRepository.SaveAsync();
        }

        private static decimal CalculateTransactionAmount(List<Team> actualTeams)
        {
            var transactionAmount = 0M;

            foreach (var team in actualTeams)
            {
                if (team.PromotionalCode != null)
                {
                    if (team.PromotionalCode.FlatRateCost != null)
                    {
                        transactionAmount += team.PromotionalCode.FlatRateCost.Value;
                    }
                    else if (team.PromotionalCode.DiscountPercentage != null && team.Division.Cost != null)
                    {
                        var discountAmount = (team.Division.Cost.Value * (team.PromotionalCode.DiscountPercentage.Value * .01M));
                        transactionAmount += (team.Division.Cost.Value - discountAmount);
                    }
                }
                else
                {
                    if (team.Division.Cost != null)
                    {
                        transactionAmount += team.Division.Cost.Value;
                    }
                }
            }

            return transactionAmount;
        }

        private bool Validate(Team team)
        {
            var validationErrors = new List<string>();
            bool isValid = false;

            validationErrors.AddRange(ValidatePlayerCount(team));
            validationErrors.AddRange(ValidateGender(team));
            validationErrors.AddRange(ValidatePlayers(team));

            if (validationErrors.Count == 0)
            {
                isValid = true;
            }

            return isValid;
        }

        private List<string> ValidatePlayerCount(Team team)
        {
            List<string> errorMessages = new List<string>();

            if (team.Division.MinimumNumberOfParticipants > team.Members.Count)
            {
                errorMessages.Add("Your team needs to have more players before it is ready for competition.");
            }

            if (team.Members.Count > team.Division.MaximumNumberOfParticipants)
            {
                errorMessages.Add("Your team has too many players. You need to delete some before it is ready for competition.");
            }

            return errorMessages;
        }

        private List<string> ValidateGender(Team team)
        {
            List<string> errorMessages = new List<string>();

            if (team.Gender.Name == "Female")
            {
                if (team.Members.Count(p => p.Gender != "F") > 0)
                {
                    errorMessages.Add("Your team is registered as a Female team and you have a player who is not Female.");
                }
            }

            if (team.Gender.Name == "Male")
            {
                if (team.Members.Count(p => p.Gender != "M") > 0)
                {
                    errorMessages.Add("Your team is registered as a Male team and you have a player who is not Male.");
                }
            }

            if (team.Gender.Name == "Co-Ed")
            {
                if (team.Members.Count(p => p.Gender == "M") == 0 || team.Members.Count(p => p.Gender == "F") == 0)
                {
                    errorMessages.Add("Your team is registered as a Co-Ed team and you either don't have a Male or Female on the roster.");
                }
            }

            return errorMessages;
        }

        private List<string> ValidatePlayers(Team team)
        {
            List<string> errorMessages = new List<string>();

            foreach (var participant in team.Members)
            {
                if (team.Division.IsGradeBased.HasValue && team.Division.IsGradeBased.Value)
                {
                    // check grades
                    if (team.Division.GradeLowerLimit.Value > participant.Grade)
                    {
                        errorMessages.Add($"{participant.FirstName} {participant.LastName} is entering a grade too young to play in the team's division.");
                    }
                    if (team.Division.GradeUpperLimit.Value < participant.Grade)
                    {
                        errorMessages.Add($"{participant.FirstName} {participant.LastName} is entering a grade too old to play in the team's division.");
                    }
                }
                if (team.Division.IsHeightBased.HasValue && team.Division.IsHeightBased.Value)
                {
                    // check height
                    if (team.Division.HeightLowerLimit.Value > (participant.HeightFeet * 12 + participant.HeightInches))
                    {
                        errorMessages.Add($"{participant.FirstName} {participant.LastName} is too short to play in the team's division.");
                    }
                    if (team.Division.HeightUpperLimit.Value < (participant.HeightFeet * 12 + participant.HeightInches))
                    {
                        errorMessages.Add($"{participant.FirstName} {participant.LastName} is too tall to play in the team's division.");
                    }
                }
            }

            // foreach (var participant in activeParticipants)
            // {
            //    if (participant.IsChargedPlayer && participant.PaymentTransactionId == null)
            //    {
            //        errorMessages.Add("You have added a player that hasn't been paid for yet. You will need to pay for that player before your team is valid.");
            //        break;
            //    }
            // }

            return errorMessages;
        }

        private async Task SendConfirmationEmail(List<int> teamIds)
        {
            var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary());
            int managerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await UserManager.FindByIdAsync(managerId.ToString());
            IQueryable<Team> teams = TeamRepository.AllIncluding(t => t.Division, t => t.CompetitionLevel, t => t.Members, t => t.Gender).Where(t => t.ManagerId == managerId && teamIds.Contains(t.Id));
            List<TransactionHistory> transactionHistory = TransactionHistoryRepository.All.ToList();

            foreach (var team in teams)
            {
                var teamViewModel = Mapper.Map<RegisteredTeamViewModel>(team);

                viewDataDictionary.Model = new TeamRegisteredViewModel
                {
                    EmailAddress = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Members = Mapper.Map<IList<RegisteredParticipantViewModel>>(team.Members),
                    Team = teamViewModel,
                    DateRegistered = transactionHistory.SingleOrDefault(t => t.TransactionId == team.PaymentTransactionId).TransactionDate
                };

                var message = await EmailViewRenderer.RenderAsync("TeamRegistered", viewDataDictionary);
                await EmailSender.SendEmailAsync(user.Email, $"{AppSettings.TournamentName} Confirmation - {team.Name}", message);
            }
        }
    }
}
