namespace EMS.Domain.Models
{
    public class PromotionalCode : EventInfoEntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? DiscountPercentage { get; set; }
        public int? FlatRateCost { get; set; }
        public int? NumberOfCodes { get; set; }
        public bool? IsPlayerCode { get; set; }
    }
}