namespace RTL.TvMazeScraper.Infastructure.Models.Api
{
    public class ShowsApiResponseModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public IEnumerable<CastApiResponseModel> Cast { get; set; } = [];
    }
}
