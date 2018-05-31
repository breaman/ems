using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.Models
{
    public class AppSettings
    {
        public string SiteName { get; set; }
        public string TournamentName { get; set; }
        public string ContactEmail { get; set; }
        public ThemeOptions ThemeOptions { get; set; } = new ThemeOptions();
        public EmailConfig Email { get; set; }
        public PaymentConfig Payment { get; set; }
    }
}
