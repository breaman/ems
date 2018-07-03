using System;

namespace EMS.Domain.Models
{
    public class Game : EventInfoFingerPrintingEntityBase
    {
        public int? BracketId { get; set; }
        public int? FieldId { get; set; }
        public Field Field { get; set; }
        public DateTimeOffset GameTime { get; set; }
        public int? Team1Id { get; set; }
        public int? Team2Id { get; set; }
        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }
        public int? GameNumber { get; set; }
    }
}