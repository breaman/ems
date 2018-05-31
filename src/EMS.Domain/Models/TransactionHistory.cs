using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class TransactionHistory : EntityBase
    {
        public int ManagerId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
