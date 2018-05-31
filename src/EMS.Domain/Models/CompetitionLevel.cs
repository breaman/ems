using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class CompetitionLevel : EntityBase
    {
        public int? DivisionId { get; set; }
        public int? GenderId { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
