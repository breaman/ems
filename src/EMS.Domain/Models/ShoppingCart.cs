using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class ShoppingCart : EntityBase
    {
        public int ManagerId { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public int? ParticipantId { get; set; }
        public Participant Participant { get; set; }
    }
}
