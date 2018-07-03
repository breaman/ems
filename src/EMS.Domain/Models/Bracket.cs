namespace EMS.Domain.Models
{
    public class Bracket : EventInfoEntityBase
    {
        public int? Number { get; set; }
        public string Name { get; set; }
        public int? FieldId { get; set; }
        public Field Field { get; set; }
        public string Referees { get; set; }
    }
}