using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Division : EntityBase
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
        public decimal? Cost { get; set; }
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
    }
}
