using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Gender : EntityBase
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public int? DivisionId { get; set; }
    }
}
