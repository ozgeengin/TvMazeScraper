namespace RTL.TvMazeScraper.Infastructure.Models.Api
{
    public class CastApiResponseModel
    {
        public int Id { get; set; }
        public required PersonApiResponseModel Person { get; set; }
    }
}
