using RTL.TvMazeScraper.Infastructure.Models.Api;

namespace RTL.TvMazeScraper.Infastructure.Services.Interfaces
{
    public interface ITvMazeSyncService
    {
        Task SyncShowsToDatabaseAsync(IEnumerable<ShowsApiResponseModel> shows);
    }
}
