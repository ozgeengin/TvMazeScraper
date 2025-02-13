namespace RTL.TvMazeScraper.Domain.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
