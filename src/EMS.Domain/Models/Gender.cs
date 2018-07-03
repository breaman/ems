namespace EMS.Domain.Models
{
    public class Gender : EventInfoEntityBase
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public int? DivisionId { get; set; }
        public int? MinimumNumberOfFemaleParticipants { get; set; }
        public int? MinimumNumberOfMaleParticipants { get; set; }
    }
}