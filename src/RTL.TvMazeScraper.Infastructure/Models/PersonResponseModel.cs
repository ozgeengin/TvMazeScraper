namespace RTL.TvMazeScraper.Infastructure.Models
{
    public class PersonResponseModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}