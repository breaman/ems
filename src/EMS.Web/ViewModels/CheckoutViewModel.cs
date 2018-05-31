using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public class TeamInfo
        {
            public int TeamId { get; set; }
            public string Name { get; set; }
            public int PlayerCount { get; set; }
            public string Division { get; set; }
            public string PromoCode { get; set; }
            public decimal Cost { get; set; }
        }
        public class CCInfo
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string CountryCode { get; set; }
            public string CardNumber { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public string Cvv { get; set; }
        }

        public List<TeamInfo> Teams { get; set; }
        public CCInfo CcInfo { get; set; }
        public bool AgreesToTerms { get; set; }
        public bool Success { get; set; }
        public bool UseDefaultAddress { get; set; }
        public List<string> Errors { get; set; }
    }
}
