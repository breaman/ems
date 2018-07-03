namespace EMS.Domain.Models
{
    public abstract class EventInfoFingerPrintingEntityBase : FingerPrintingEntityBase
    {
        public int EventInfoId { get; set; }
        public EventInfo EventInfo { get; set; }
    }
}