namespace RTL.TvMazeScraper.Infastructure.Data.Entities
{
    public class ShowEntity : BaseEntity
    {
        public required string Name { get; set; }
        public List<PersonEntity> Cast = [];
    }
}
