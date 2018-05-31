using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.ViewModels
{
    public class RegisteredTeamViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string GenderName { get; set; }
        public string CompetitionLevelName { get; set; }
    }
}
