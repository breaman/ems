namespace EMS.Domain.Models
{
    public class Participant : EventInfoFingerPrintingEntityBase
    {
        public Team Team { get; set; }
        public int? TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}