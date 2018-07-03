using System;

namespace EMS.Domain.Models
{
    public class TransactionHistory : EventInfoEntityBase
    {
        public int ManagerId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}