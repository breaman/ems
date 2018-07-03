using System;
using System.Collections.Generic;

namespace EMS.Domain.Models
{
    public class Team : EventInfoFingerPrintingEntityBase
    {
        public int ManagerId { get; set; }
        public User Manager { get; set; }
        public string Name { get; set; }
        public int? DivisionId { get; set; }
        public Division Division { get; set; }
        public int? GenderId { get; set; }
        public Gender Gender { get; set; }
        public int? CompetitionLevelId { get; set; }
        public CompetitionLevel CompetitionLevel { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public IList<Participant> Members { get; set; }
        public string PaymentTransactionId { get; set; }
        public int? PromotionalCodeId { get; set;}
        public PromotionalCode PromotionalCode { get; set; }
        public int? BracketId { get; set; }
        public Bracket Bracket { get; set; }
    }
}