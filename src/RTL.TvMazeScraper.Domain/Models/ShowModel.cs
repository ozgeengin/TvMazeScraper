namespace RTL.TvMazeScraper.Domain.Models
{
    public class ShowModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<PersonModel> Cast { get; set; } = [];
    }
}
