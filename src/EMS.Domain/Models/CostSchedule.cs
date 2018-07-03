using System;

namespace EMS.Domain.Models
{
    public class CostSchedule : EventInfoEntityBase
    {
        public int? DivisionId { get; set; }
        public Division Division { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public decimal? Cost { get; set; }
    }
}