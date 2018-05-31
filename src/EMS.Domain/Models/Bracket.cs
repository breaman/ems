using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Bracket : EntityBase
    {
        public int? Number { get; set; }
        public string Name { get; set; }
        public int? FieldId { get; set; }
        public Field Field { get; set; }
        public string Referees { get; set; }
    }
}
