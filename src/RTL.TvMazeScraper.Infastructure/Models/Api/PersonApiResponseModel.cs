namespace RTL.TvMazeScraper.Infastructure.Models.Api
{
    public class PersonApiResponseModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}