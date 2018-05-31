using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Domain.Models;

namespace EMS.Web.ViewModels
{
    public class TeamViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public GenderViewModel Gender { get; set; }
        public DivisionViewModel Division { get; set; }
        public CompetitionLevelViewModel CompetitionLevel { get; set; }
        public List<ParticipantViewModel> Players { get; set; }
        public string PromotionalCode { get; set; }
        public string Status { get; set; }
        public List<string> ValidationErrors { get; set; }

        public void Validate(Team team)
        {
            ValidationErrors = new List<string>();

            ValidationErrors.AddRange(ValidatePlayerCount(team.Division));
            ValidationErrors.AddRange(ValidateGender());
            ValidationErrors.AddRange(ValidatePlayers(team.Division));

            if (ValidationErrors.Count > 0)
            {
                Status = "Not Ready";
            }
            else
            {
                Status = "Ready For Payment";
            }
            if (team.PaymentTransactionId != null)
            {
                Status = "Paid";
            }
        }

        private List<string> ValidatePlayerCount(Division division)
        {
            List<string> errorMessages = new List<string>();

            if (division.MinimumNumberOfParticipants > Players.Count)
            {
                errorMessages.Add("Your team needs to have more players before it is ready for competition.");
            }

            if (Players.Count > division.MaximumNumberOfParticipants)
            {
                errorMessages.Add("Your team has too many players. You need to delete some before it is ready for competition.");
            }

            return errorMessages;
        }

        private List<string> ValidateGender()
        {
            List<string> errorMessages = new List<string>();

            if (Gender.Name == "Female")
            {
                if (Players.Count(p => p.Gender != "F") > 0)
                {
                    errorMessages.Add("Your team is registered as a Female team and you have a player who is not Female.");
                }
            }

            if (Gender.Name == "Male")
            {
                if (Players.Count(p => p.Gender != "M") > 0)
                {
                    errorMessages.Add("Your team is registered as a Male team and you have a player who is not Male.");
                }
            }

            if (Gender.Name == "Co-Ed")
            {
                if (Players.Count(p => p.Gender == "M") == 0 || Players.Count(p => p.Gender == "F") == 0)
                {
                    errorMessages.Add("Your team is registered as a Co-Ed team and you either don't have a Male or Female on the roster.");
                }
            }

            return errorMessages;
        }

        private List<string> ValidatePlayers(Division division)
        {
            List<string> errorMessages = new List<string>();

            foreach (var participant in Players)
            {
                if (division.IsGradeBased.HasValue && division.IsGradeBased.Value)
                {
                    // check grades
                    if (division.GradeLowerLimit.Value > participant.NextGrade)
                    {
                        errorMessages.Add($"{participant.FirstName} {participant.LastName} is entering a grade too young to play in the team's division.");
                    }
                    if (division.GradeUpperLimit.Value < participant.NextGrade)
                    {
                        errorMessages.Add($"{participant.FirstName} {participant.LastName} is entering a grade too old to play in the team's division.");
                    }
                }
                if (division.IsHeightBased.HasValue && division.IsHeightBased.Value)
                {
                    // check height
                    if (division.HeightLowerLimit.Value > (participant.HeightFeet * 12 + participant.HeightInches))
                    {
                        errorMessages.Add($"{participant.FirstName} {participant.LastName} is too short to play in the team's division.");
                    }
                    if (division.HeightUpperLimit.Value < (participant.HeightFeet * 12 + participant.HeightInches))
                    {
                        errorMessages.Add($"{participant.FirstName} {participant.LastName} is too tall to play in the team's division.");
                    }
                }
            }

            //foreach (var participant in activeParticipants)
            //{
            //    if (participant.IsChargedPlayer && participant.PaymentTransactionId == null)
            //    {
            //        errorMessages.Add("You have added a player that hasn't been paid for yet. You will need to pay for that player before your team is valid.");
            //        break;
            //    }
            //}

            return errorMessages;
        }
    }
}
