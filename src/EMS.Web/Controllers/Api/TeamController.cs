using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EMS.Domain.Models;
using EMS.Domain.Abstract;
using EMS.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
using System.Reflection;
using EMS.Web.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        public ITeamRepository TeamRepository { get; }
        public IParticipantRepository ParticipantRepository { get; }
        public ITournamentParameterRepository TournamentParameterRepository { get; }
        public IDivisionRepository DivisionRepository { get; }
        public IGenderRepository GenderRepository { get; }
        public IPromotionalCodeRepository PromoCodeRepository { get; }
        public IMapper Mapper { get; }
        public TeamController(ITeamRepository teamRepository, IParticipantRepository participantRepository, IMapper mapper,
            ITournamentParameterRepository tournamentParameterRepository, IDivisionRepository divisionRepository, IGenderRepository genderRepository,
            IPromotionalCodeRepository promoCodeRepository)
        {
            TeamRepository = teamRepository;
            ParticipantRepository = participantRepository;
            TournamentParameterRepository = tournamentParameterRepository;
            DivisionRepository = divisionRepository;
            GenderRepository = genderRepository;
            PromoCodeRepository = promoCodeRepository;
            Mapper = mapper;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ResponseCache(NoStore=true)]
        public async Task<IActionResult> Get(int id)
        {
            var managerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Team team = await TeamRepository.All.Include(t => t.Division).Include(t => t.Gender).Include(t => t.CompetitionLevel).Include(t => t.Members).SingleOrDefaultAsync(t => t.Id == id && t.ManagerId == managerId);

            var teamViewModel = Mapper.Map<TeamViewModel>(team);

            teamViewModel.Validate(team);

            return Json(teamViewModel);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TeamViewModel teamViewModel)
        {
            IActionResult result = null;
            var managerId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Team team = null;
            bool hasError = false;
            PromotionalCode promoCode = null;

            await ValidateTeamName(teamViewModel);

            await ValidatePlayerData(teamViewModel);

            if (!string.IsNullOrWhiteSpace(teamViewModel.PromotionalCode))
            {
                promoCode = await ValidatePromoCode(teamViewModel);
            }

            if (ModelState.IsValid)
            {
                // perform update of team/player data
                if (teamViewModel.Id.HasValue && teamViewModel.Id.Value > 0)
                {
                    team = TeamRepository.All.SingleOrDefault(t => t.ManagerId == managerId && t.Id == teamViewModel.Id.Value);
                    if (team == null)
                    {
                        // log exception, notify user they are trying to edit a team they are not a manager of
                        var error = new ApiStatusResponse
                        {
                            Messages = new List<string>(new string[] { "You are trying to edit a team that you do not manage." }),
                            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden
                        };
                        Response.StatusCode = error.StatusCode;
                        result = new ObjectResult(error);
                        hasError = true;
                    }
                    else
                    {
                        Mapper.Map(teamViewModel, team);
                        await UpdateParticipants(teamViewModel, managerId);
                    }
                }
                // add new team/players
                else
                {
                    // map team information
                    team = Mapper.Map<Team>(teamViewModel);
                    team.ManagerId = managerId;
                    // map any player information
                    foreach (ParticipantViewModel participant in teamViewModel.Players)
                    {
                        if (team.Members == null)
                        {
                            team.Members = new List<Participant>();
                        }
                        team.Members.Add(Mapper.Map<Participant>(participant));
                    }
                }

                if (!hasError)
                {
                    if (promoCode != null)
                    {
                        team.PromotionalCodeId = promoCode.Id;
                    }
                    TeamRepository.InsertOrUpdate(team);
                    try
                    {
                        await TeamRepository.SaveAsync();
                        var teamId = team.Id;
                        team = await TeamRepository.All.Include(t => t.Division).Include(t => t.Gender).Include(t => t.CompetitionLevel).Include(t => t.Members).Where(t => t.ManagerId == managerId && t.Id == teamId).SingleOrDefaultAsync();

                        if (team.Members == null)
                        {
                            team.Members = new List<Participant>();
                        }
                        teamViewModel = Mapper.Map<TeamViewModel>(team);

                        teamViewModel.Validate(team);

                        result = Json(teamViewModel);
                    }
                    catch (Exception e)
                    {
                        // need to log exception and then let the user know that their team could not be saved.
                        var error = new ApiStatusResponse
                        {
                            Messages = new List<string>(new string[] { e.Message }),
                            StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest
                        };
                        Response.StatusCode = error.StatusCode;
                        result = new ObjectResult(error);
                    }
                }
            }
            else
            {
                // need to add all model state errors
                var error = new ApiStatusResponse
                {
                    Messages = new List<string>(),
                    StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity
                };
                foreach (var key in ModelState.Keys)
                {
                    foreach (var modelStateError in ModelState[key].Errors)
                    {
                        var player = "";
                        switch (key)
                        {
                            case string k when (k.IndexOf("players[0]", StringComparison.CurrentCultureIgnoreCase) > -1):
                                player = "Captain";
                                break;
                            case string k when (k.IndexOf("players[1]", StringComparison.CurrentCultureIgnoreCase) > -1):
                                player = "Player #2";
                                break;
                            case string k when (k.IndexOf("players[2]", StringComparison.CurrentCultureIgnoreCase) > -1):
                                player = "Player #3";
                                break;
                            case string k when (k.IndexOf("players[3]", StringComparison.CurrentCultureIgnoreCase) > -1):
                                player = "Player #4";
                                break;
                        }
                        error.Messages.Add($"{player} - {modelStateError.ErrorMessage}");
                    }
                }
                Response.StatusCode = error.StatusCode;
                result = new ObjectResult(error);
            }

            return result;
        }

        private async Task<PromotionalCode> ValidatePromoCode(TeamViewModel teamViewModel)
        {
            var promoCode = await PromoCodeRepository.All.SingleOrDefaultAsync(pc => pc.PromoCode == teamViewModel.PromotionalCode);
            Team team = null;
            int numberOfCodesUsed;

            if (teamViewModel.Id != null && teamViewModel.Id.Value > 0)
            {
                team = await TeamRepository.FindAsync(teamViewModel.Id.Value);
            }
            if (team != null && team.PromotionalCodeId != null)
            {
                ModelState.AddModelError("", "You have already entered a promotional code for this team. Only one code per team is allowed.");
            }
            if (promoCode == null)
            {
                ModelState.AddModelError("PromoCode", "You have entered an invalid code, please verify the code you were given and try again.");
            }
            else
            {
                numberOfCodesUsed = await TeamRepository.All.CountAsync(t => t.PromotionalCodeId == promoCode.Id);
                if (numberOfCodesUsed >= promoCode.NumberOfCodes)
                {
                    ModelState.AddModelError("", "This promotional code has reached its limit of uses, please enter a different code.");
                }
            }

            return promoCode;
        }
        private async Task ValidateTeamName(TeamViewModel teamViewModel)
        {
            // check for the name starting with 'The' or 'Team'. Also make sure the name is unique.
            if (teamViewModel.Name.StartsWith("The ", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Name", "A team name cannot start with 'The '.");
            }
            if (teamViewModel.Name.StartsWith("Team ", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Name", "A team name cannot start with 'Team '.");
            }
            if (await (TeamRepository.All.CountAsync(t => t.Name == teamViewModel.Name && t.Id != teamViewModel.Id && !t.IsDeleted)) > 0)
            {
                ModelState.AddModelError("Name", "A team with that name already exists. Please change the team name and submit again.");
            }
        }

        private async Task ValidatePlayerData(TeamViewModel teamViewModel)
        {
            foreach (var participant in teamViewModel.Players)
            {
                ValidateAge(participant.Age, participant.Birthdate);
                await ValidateAgainstTeamCriteria(teamViewModel, participant);
            }
        }

        private void ValidateAge(int age, DateTime birthDate)
        {
            DateTime ageComparisonDate = Convert.ToDateTime(TournamentParameterRepository.All.Single(tp => tp.Name == "TournamentDate").Value);
            int year = ageComparisonDate.Year - birthDate.Year;

            if (age < 0 || age > 100)
            {
                ModelState.AddModelError("Age", "Each player must be between 0 and 100 years old.");
            }

            if ((birthDate.Month > ageComparisonDate.Month) ||
                ((birthDate.Month == ageComparisonDate.Month) && (birthDate.Day > ageComparisonDate.Day)))
            {
                year--;
            }
            if (year != age)
            {
                ModelState.AddModelError("Age", $"Age of {age} and birthdate of {birthDate} do not match up.");
            }
        }

        private async Task ValidateAgainstTeamCriteria(TeamViewModel viewModel, ParticipantViewModel participant)
        {
            await ValidatePlayerToDivision(viewModel.Division.Id, participant);
            await ValidatePlayerToGender(viewModel.Gender.Id, participant);
        }

        private async Task ValidatePlayerToGender(int id, ParticipantViewModel participant)
        {
            var gender = await GenderRepository.FindAsync(id);
            if (gender.Name == "Female")
            {
                if (!(participant.Gender == "F"))
                {
                    ModelState.AddModelError("Gender", "Your team is registered for a female league. In order to play co-ed, you must register the team as a co-ed team.");
                }
            }
            if (gender.Name == "Male")
            {
                if (!(participant.Gender == "M"))
                {
                    ModelState.AddModelError("Gender", "Your team is registered for a male league. In order to play co-ed, you must register the team as a co-ed team.");
                }
            }
        }

        private async Task ValidatePlayerToDivision(int id, ParticipantViewModel participant)
        {
            var division = await DivisionRepository.FindAsync(id);
            
            if (division.IsGradeBased.HasValue && division.IsGradeBased.Value)
            {
                if (participant.NextGrade < division.GradeLowerLimit.Value || participant.NextGrade > division.GradeUpperLimit.Value)
                {
                    ModelState.AddModelError("Grade", $"Your team is registered for players between grades {division.GradeLowerLimit.Value} and {division.GradeUpperLimit.Value}.");
                }
            }
            if (division.IsHeightBased.HasValue && division.IsHeightBased.Value)
            {
                int overallHeight = participant.HeightFeet * 12 + participant.HeightInches;
                if (overallHeight < division.HeightLowerLimit.Value || overallHeight > division.HeightUpperLimit.Value)
                {
                    ModelState.AddModelError("HeightFeet", $"Your team is registered for players between {division.HeightLowerLimit.Value} and {division.HeightUpperLimit.Value} inches.");
                }
            }
        }

        private async Task UpdateParticipants(TeamViewModel teamViewModel, int managerId)
        {
            List<Participant> participants = null;
            participants = await ParticipantRepository.All.Where(p => p.TeamId == teamViewModel.Id).ToListAsync();
            // if the team didn't have any participants before, just map all participants over
            if (participants.Count == 0)
            {
                // map any player information
                foreach (ParticipantViewModel participant in teamViewModel.Players)
                {
                    Participant newParticipant = null;
                    try
                    {
                        newParticipant = Mapper.Map<Participant>(participant);
                    }
                    catch(Exception e)
                    {
                        var exceptionMessage = e.Message;
                    }
                    newParticipant.TeamId = teamViewModel.Id;
                    ParticipantRepository.InsertOrUpdate(newParticipant);
                }
            }
            else
            {
                // if the team had participants, update/add participant information
                foreach (ParticipantViewModel participant in teamViewModel.Players)
                {
                    var existingParticipant = participants.Find(p => p.Id == participant.Id);
                    // if found, then update
                    if (existingParticipant != null)
                    {
                        Mapper.Map(participant, existingParticipant);
                        existingParticipant.TeamId = existingParticipant.Team.Id;
                        ParticipantRepository.InsertOrUpdate(existingParticipant);
                    }
                    else // insert new participant
                    {
                        var newParticipant = Mapper.Map<Participant>(participant);
                        newParticipant.TeamId = teamViewModel.Id;
                        ParticipantRepository.InsertOrUpdate(newParticipant);
                    }
                }
                // check for deleted players
                foreach (Participant participant in participants)
                {
                    if (teamViewModel.Players.Count(p => p.Id == participant.Id) == 0)
                    {
                        participant.IsDeleted = true;
                        participant.DeletedOn = DateTime.Now;
                        participant.DeletedBy = managerId;
                        ParticipantRepository.InsertOrUpdate(participant);
                    }
                }
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private void MapAllNonNullFields<V, T>(V vm, T actualPost)
        {
            Type postType = actualPost.GetType();
            foreach (PropertyInfo property in vm.GetType().GetProperties())
            {
                if (property.GetValue(vm) != null)
                {
                    var postProperty = postType.GetProperty(property.Name);
                    postProperty.SetValue(actualPost, property.GetValue(vm));
                }
            }
        }
    }
}
