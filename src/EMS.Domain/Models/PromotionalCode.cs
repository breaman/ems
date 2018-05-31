using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class PromotionalCode : EntityBase
    {
        public string PromoCode { get; set; }
        public string Description { get; set; }
        public int? DiscountPercentage { get; set; }
        public int? FlatRateCost { get; set; }
        public int? NumberOfCodes { get; set; }
        public bool? IsPlayerCode { get; set; }
    }
}
