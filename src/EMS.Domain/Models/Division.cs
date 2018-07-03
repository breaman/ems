using System.Collections.Generic;

namespace EMS.Domain.Models
{
    public class Division : EventInfoEntityBase
    {
        public string Name { get; set; }
        public bool? IsGradeBased { get; set; }
        public bool? IsHeightBased { get; set; }
        public int? GradeLowerLimit { get; set; }
        public int? GradeUpperLimit { get; set; }
        public int? HeightLowerLimit { get; set; }
        public int? HeightUpperLimit { get; set; }
        public int? MinimumNumberOfParticipants { get; set; }
        public int? MaximumNumberOfParticipants { get; set; }
        public List<CostSchedule> Cost { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public int? CustomKey { get; set; }
    }
}