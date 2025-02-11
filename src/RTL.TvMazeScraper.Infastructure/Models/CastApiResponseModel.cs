namespace RTL.TvMazeScraper.Infastructure.Models
{
    internal class CastApiResponseModel
    {
        public int Id { get; set; }
        public required PersonResponseModel Person { get; set; }
    }
}
