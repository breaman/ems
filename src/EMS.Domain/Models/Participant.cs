using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Participant : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string DayTimePhone { get; set; }
        public string EveningPhone { get; set; }
        public string EmailAddress { get; set; }
        public string Employer { get; set; }
        public string EmployerWebsite { get; set; }
        public string WorkPhone { get; set; }
        public string WorkEmail { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? Age { get; set; }
        public int? HeightFeet { get; set; }
        public int? HeightInches { get; set; }
        public string Gender { get; set; }
        public int? Grade { get; set; }
        public int? PlayingExperience { get; set; }
        public int? PlayingFrequency { get; set; }
        public string TShirtSize { get; set; }
        public bool? PlayedLastYear { get; set; }
        public bool? IsChargedPlayer { get; set; }
        public string OtherCountry { get; set; }
        public string OtherStateProvince { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

    }
}
