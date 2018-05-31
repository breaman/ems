using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class TournamentParameter : EntityBase
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
