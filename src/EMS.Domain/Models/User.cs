using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Domain.Abstract;
using Microsoft.AspNetCore.Identity;

namespace EMS.Domain.Models
{
    public class User : IdentityUser<int>, IEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string OtherCountry { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string OtherStateProvince { get; set; }
        public string Zip { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public DateTimeOffset? MemberSince { get; set; }
    }
}
