using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.ViewModels
{
    public class TeamRegisteredViewModel
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<RegisteredParticipantViewModel> Members { get; set; }
        public RegisteredTeamViewModel Team { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
