using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EMS.Domain.Abstract;
using EMS.Domain.Models;
using EMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.Web.Controllers
{
    public class TransactionsController : Controller
    {
        public IParticipantRepository ParticipantRepository { get; private set; }
        public ITeamRepository TeamRepository { get; private set; }
        public ITransactionHistoryRepository TransactionRepository { get; private set; }

        public TransactionsController(ITeamRepository teamRepository, IParticipantRepository playerRepository,
            ITransactionHistoryRepository transactionRepository)
        {
            TeamRepository = teamRepository;
            ParticipantRepository = playerRepository;
            TransactionRepository = transactionRepository;
        }

        public async Task<ActionResult> Index()
        {
            List<AccountTransactionViewModel> viewModel = new List<AccountTransactionViewModel>();
            int managerId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Team> teams;
            AccountTransactionViewModel currentTransaction;
            List<TransactionHistory> transactions = await TransactionRepository.All.Where(t => t.ManagerId == managerId).ToListAsync();

            foreach (TransactionHistory transaction in transactions)
            {
                currentTransaction = new AccountTransactionViewModel();
                currentTransaction.Details = new List<TransactionInformationViewModel>();
                currentTransaction.TransactionId = transaction.TransactionId;

                teams = await TeamRepository.All.Where(t => t.PaymentTransactionId == transaction.TransactionId).ToListAsync();
                foreach (Team team in teams)
                {
                    currentTransaction.Details.Add(new TransactionInformationViewModel()
                    {
                        Date = transaction.TransactionDate,
                        Id = team.Id,
                        Name = team.Name,
                        Type = "Team",
                        TypeDescription = "TEAM PAYMENT"
                    });
                }

                viewModel.Add(currentTransaction);
            }

            return View(viewModel);
        }
    }
}
