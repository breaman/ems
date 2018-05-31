using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EMS.Domain.Abstract;
using EMS.Domain.Models;
using EMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS.Web.Controllers
{
    public class AdminController : Controller
    {
        public UserManager<User> UserManager { get; }
        public ITeamRepository TeamRepository { get; private set; }
        public AdminController(ITeamRepository teamRepository, UserManager<User> userManager)
        {
            UserManager = userManager;
            TeamRepository = teamRepository;
        }

        [Authorize(Roles = "Administrator,Support")]
        public async Task<IActionResult> CompleteTeamReport()
        {
            List<Team> teams = await TeamRepository.AllIncluding(t => t.Members, t => t.Manager, t => t.Division, t => t.CompetitionLevel, t => t.Gender, t => t.PromotionalCode).Where(t => !t.IsDeleted && t.PaymentTransactionId != null && t.PaymentTransactionId != "ROLLBACK").ToListAsync();
            MemoryStream memoryStream = new MemoryStream();
            XLWorkbook workbook = new XLWorkbook();

            AddConsolidatedTeamReport(workbook, teams);
            AddFullTeamReport(workbook, teams);

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CompleteTeams.xlsx");

            return null;
        }
        
        private void AddFullTeamReport(XLWorkbook workbook, List<Team> teams)
        {
            IXLWorksheet completedTeams = workbook.Worksheets.Add("FullPlayerReport");
            int currentRow = 2;

            #region Barefoot3v3
            //completedTeams.Cell("A1").Value = "Name";
            //completedTeams.Cell("B1").Value = "Number";
            //completedTeams.Cell("C1").Value = "Division";
            //completedTeams.Cell("D1").Value = "CompetitionLevel";
            //completedTeams.Cell("E1").Value = "Gender";
            //completedTeams.Cell("F1").Value = "Player First Name";
            //completedTeams.Cell("G1").Value = "Player Last Name";
            //completedTeams.Cell("H1").Value = "Player Age";
            //completedTeams.Cell("I1").Value = "Player Gender";
            //completedTeams.Cell("J1").Value = "T-Shirt Size";
            //completedTeams.Cell("K1").Value = "Address";
            //completedTeams.Cell("L1").Value = "Address2";
            //completedTeams.Cell("M1").Value = "City";
            //completedTeams.Cell("N1").Value = "State";
            //completedTeams.Cell("O1").Value = "Zip";
            //completedTeams.Cell("P1").Value = "Email Address";
            //completedTeams.Cell("Q1").Value = "Phone Number";
            #endregion

            completedTeams.Cell("A1").Value = "Name";
            completedTeams.Cell("B1").Value = "Number";
            completedTeams.Cell("C1").Value = "Division";
            completedTeams.Cell("D1").Value = "CompetitionLevel";
            completedTeams.Cell("E1").Value = "Gender";
            completedTeams.Cell("F1").Value = "Player First Name";
            completedTeams.Cell("G1").Value = "Player Last Name";
            completedTeams.Cell("H1").Value = "Player Age";
            completedTeams.Cell("I1").Value = "Player Grade";
            completedTeams.Cell("J1").Value = "Player Experience";
            completedTeams.Cell("K1").Value = "Player Frequency";
            completedTeams.Cell("L1").Value = "Player Height";
            completedTeams.Cell("M1").Value = "Player Gender";
            completedTeams.Cell("N1").Value = "T-Shirt Size";
            completedTeams.Cell("O1").Value = "Height in Inches";
            completedTeams.Cell("P1").Value = "Experience Number";
            completedTeams.Cell("Q1").Value = "Frequency Number";
            completedTeams.Cell("R1").Value = "Address";
            completedTeams.Cell("S1").Value = "Address2";
            completedTeams.Cell("T1").Value = "City";
            completedTeams.Cell("U1").Value = "State";
            completedTeams.Cell("V1").Value = "Zip";
            completedTeams.Cell("W1").Value = "Email Address";
            completedTeams.Cell("X1").Value = "Phone Number";

            foreach (Team team in teams)
            {
                foreach (Participant player in team.Members.Where(p => !p.IsDeleted))
                {
                    #region Barefoot3v3
                    //completedTeams.Cell("A" + currentRow).Value = team.Name;
                    //completedTeams.Cell("B" + currentRow).Value = team.Id.ToString("0000");
                    //completedTeams.Cell("C" + currentRow).Value = team.Division.Name;
                    //completedTeams.Cell("D" + currentRow).Value = team.CompetitionLevel.Name;
                    //completedTeams.Cell("E" + currentRow).Value = team.Gender.Name;
                    //completedTeams.Cell("F" + currentRow).Value = player.FirstName;
                    //completedTeams.Cell("G" + currentRow).Value = player.LastName;
                    //completedTeams.Cell("H" + currentRow).Value = player.Age;
                    //completedTeams.Cell("I" + currentRow).Value = player.Gender;
                    //completedTeams.Cell("J" + currentRow).Value = player.TShirtSize;
                    //completedTeams.Cell("K" + currentRow).Value = player.Address1;
                    //completedTeams.Cell("L" + currentRow).Value = player.Address2;
                    //completedTeams.Cell("M" + currentRow).Value = player.City;
                    //completedTeams.Cell("N" + currentRow).Value = player.StateProvince;
                    //completedTeams.Cell("O" + currentRow).Value = player.PostalCode;
                    //completedTeams.Cell("P" + currentRow).Value = player.EmailAddress;
                    //completedTeams.Cell("Q" + currentRow).Value = player.DayTimePhone;
                    #endregion

                    completedTeams.Cell("A" + currentRow).Value = team.Name;
                    completedTeams.Cell("B" + currentRow).Value = team.Id.ToString("0000");
                    completedTeams.Cell("C" + currentRow).Value = team.Division.Name;
                    completedTeams.Cell("D" + currentRow).Value = team.CompetitionLevel.Name;
                    completedTeams.Cell("E" + currentRow).Value = team.Gender.Name;
                    completedTeams.Cell("F" + currentRow).Value = player.FirstName;
                    completedTeams.Cell("G" + currentRow).Value = player.LastName;
                    completedTeams.Cell("H" + currentRow).Value = player.Age.Value.ToString();
                    completedTeams.Cell("I" + currentRow).Value = player.Grade.Value.ToString();
                    completedTeams.Cell("J" + currentRow).Value = Utilities.PlayingExperience[player.PlayingExperience.Value];
                    completedTeams.Cell("K" + currentRow).Value = Utilities.PlayingFrequency[player.PlayingFrequency.Value];
                    completedTeams.Cell("L" + currentRow).Value = (player.HeightFeet * 12 + player.HeightInches).Value.ToString();
                    completedTeams.Cell("M" + currentRow).Value = player.Gender;
                    completedTeams.Cell("N" + currentRow).Value = player.TShirtSize;
                    completedTeams.Cell("O" + currentRow).Value = ((player.HeightFeet * 12) + player.HeightInches).Value.ToString();
                    completedTeams.Cell("P" + currentRow).Value = player.PlayingExperience.Value.ToString();
                    completedTeams.Cell("Q" + currentRow).Value = player.PlayingFrequency.Value.ToString();
                    completedTeams.Cell("R" + currentRow).Value = player.Address1;
                    completedTeams.Cell("S" + currentRow).Value = player.Address2;
                    completedTeams.Cell("T" + currentRow).Value = player.City;
                    completedTeams.Cell("U" + currentRow).Value = player.StateProvince;
                    completedTeams.Cell("V" + currentRow).Value = player.PostalCode;
                    completedTeams.Cell("W" + currentRow).Value = player.EmailAddress;
                    completedTeams.Cell("X" + currentRow).Value = player.DayTimePhone;

                    currentRow++;
                }
            }
        }

        private void AddConsolidatedTeamReport(XLWorkbook workbook, List<Team> teams)
        {
            IXLWorksheet justTeamsReport = workbook.Worksheets.Add("JustTeams");
            int currentRow = 2;

            justTeamsReport.Cell("A1").Value = "Number";
            justTeamsReport.Cell("B1").Value = "Name";
            justTeamsReport.Cell("C1").Value = "Manager First Name";
            justTeamsReport.Cell("D1").Value = "Manager Last Name";
            justTeamsReport.Cell("E1").Value = "Manager E-Mail";
            justTeamsReport.Cell("F1").Value = "Manager Phone";
            justTeamsReport.Cell("G1").Value = "Division";
            justTeamsReport.Cell("H1").Value = "CompetitionLevel";
            justTeamsReport.Cell("I1").Value = "Gender";
            justTeamsReport.Cell("J1").Value = "Promo Code";
            justTeamsReport.Cell("K1").Value = "Bracket";
            justTeamsReport.Cell("L1").Value = "Average Age";
            justTeamsReport.Cell("M1").Value = "Max Age";
            justTeamsReport.Cell("N1").Value = "Average Experience";

            foreach (Team team in teams)
            {
                justTeamsReport.Cell("A" + currentRow).Value = team.Id.ToString();
                justTeamsReport.Cell("B" + currentRow).Value = team.Name;
                justTeamsReport.Cell("C" + currentRow).Value = team.Manager.FirstName;
                justTeamsReport.Cell("D" + currentRow).Value = team.Manager.LastName;
                justTeamsReport.Cell("E" + currentRow).Value = team.Manager.Email;
                justTeamsReport.Cell("F" + currentRow).Value = team.Manager.PhoneNumber;
                justTeamsReport.Cell("G" + currentRow).Value = team.Division.Name;
                //incompleteTeams.Cell("G" + currentRow).Value = ((team.PaymentTransactionId == "ROLLBACK") ? "Yes" : "No");
                justTeamsReport.Cell("H" + currentRow).Value = team.CompetitionLevel.Name;
                justTeamsReport.Cell("I" + currentRow).Value = team.Gender.Name;
                justTeamsReport.Cell("J" + currentRow).Value = ((team.PromotionalCode != null) ? team.PromotionalCode.PromoCode : "");
                justTeamsReport.Cell("K" + currentRow).Value = ((team.Bracket != null) ? team.Bracket.Number.Value.ToString() : "");
                justTeamsReport.Cell("L" + currentRow).Value = Math.Round(team.Members.Average(p => p.Age.Value), 1).ToString();
                justTeamsReport.Cell("M" + currentRow).Value = team.Members.Max(p => p.Age).ToString();
                justTeamsReport.Cell("N" + currentRow).Value = Math.Round(team.Members.Average(p => p.PlayingExperience.Value), 1).ToString();

                currentRow++;
            }
        }

        private void AddIncompleteTeamReport(XLWorkbook workbook, List<Team> teams)
        {
            IXLWorksheet incompleteTeam = workbook.Worksheets.Add("IncompleteTeams");
            int currentRow = 2;

            incompleteTeam.Cell("A1").Value = "Number";
            incompleteTeam.Cell("B1").Value = "Name";
            incompleteTeam.Cell("C1").Value = "Manager First Name";
            incompleteTeam.Cell("D1").Value = "Manager Last Name";
            incompleteTeam.Cell("E1").Value = "Manager E-Mail";
            incompleteTeam.Cell("F1").Value = "Manager Phone";
            incompleteTeam.Cell("G1").Value = "Division";
            incompleteTeam.Cell("H1").Value = "CompetitionLevel";
            incompleteTeam.Cell("I1").Value = "Gender";

            foreach (Team team in teams)
            {
                incompleteTeam.Cell("A" + currentRow).Value = team.Id.ToString();
                incompleteTeam.Cell("B" + currentRow).Value = team.Name;
                incompleteTeam.Cell("C" + currentRow).Value = team.Manager.FirstName;
                incompleteTeam.Cell("D" + currentRow).Value = team.Manager.LastName;
                incompleteTeam.Cell("E" + currentRow).Value = team.Manager.Email;
                incompleteTeam.Cell("F" + currentRow).Value = team.Manager.PhoneNumber;
                incompleteTeam.Cell("G" + currentRow).Value = team.Division.Name;
                incompleteTeam.Cell("H" + currentRow).Value = team.CompetitionLevel.Name;
                incompleteTeam.Cell("I" + currentRow).Value = team.Gender.Name;

                currentRow++;
            }
        }
        
        [Authorize(Roles = "Administrator,Support")]
        public async Task<IActionResult> IncompleteTeamReport()
        {
            List<Team> teams = await TeamRepository.AllIncluding(t => t.Members, t => t.Manager, t => t.Division, t => t.CompetitionLevel, t => t.Gender, t => t.PromotionalCode).Where(t => !t.IsDeleted && t.PaymentTransactionId == null).ToListAsync();
            MemoryStream memoryStream = new MemoryStream();
            XLWorkbook workbook = new XLWorkbook();

            AddIncompleteTeamReport(workbook, teams);

            workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IncompleteTeams.xlsx");
        }
    }
}
