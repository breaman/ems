using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Game : EntityBase
    {
        public int? BracketId { get; set; }
        public int? FieldId { get; set; }
        public Field Field { get; set; }
        public DateTime? GameTime { get; set; }
        public int? Team1Id { get; set; }
        public int? Team2Id { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }
        public int? GameNumber { get; set; }
    }
}
