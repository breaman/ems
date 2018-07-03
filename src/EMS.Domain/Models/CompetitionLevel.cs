namespace EMS.Domain.Models
{
    public class CompetitionLevel : EventInfoEntityBase
    {
        public int? DivisionId { get; set; }
        public int? GenderId { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}