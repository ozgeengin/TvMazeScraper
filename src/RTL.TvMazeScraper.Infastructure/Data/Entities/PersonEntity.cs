namespace RTL.TvMazeScraper.Infastructure.Data.Entities
{
    public class PersonEntity : BaseEntity
    {
        public required string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public List<ShowEntity> Shows = [];
    }
}
