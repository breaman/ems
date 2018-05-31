using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Team : EntityBase
    {
        public int ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public User Manager { get; set; }
        public string Name { get; set; }
        public int? DivisionId { get; set; }
        public Division Division { get; set; }
        public int? GenderId { get; set; }
        public Gender Gender { get; set; }
        public int? CompetitionLevelId { get; set; }
        public CompetitionLevel CompetitionLevel { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? DeletedById { get; set; }
        public IList<Participant> Members { get; set; }
        public string PaymentTransactionId { get; set; }
        public int? PromotionalCodeId { get; set;}
        public PromotionalCode PromotionalCode { get; set; }
        public int? BracketId { get; set; }
        public Bracket Bracket { get; set; }
    }
}
