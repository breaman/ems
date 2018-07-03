namespace EMS.Domain.Models
{
    public abstract class EventInfoEntityBase : EntityBase
    {
        public int EventInfoId { get; set; }
        public EventInfo EventInfo { get; set; }
    }
}