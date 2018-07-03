using System;

namespace EMS.Domain.Models
{
    public abstract class FingerPrintingEntityBase : EntityBase
    {
        public int? CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}