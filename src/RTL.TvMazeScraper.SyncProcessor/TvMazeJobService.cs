using RTL.TvMazeScraper.Infastructure.Services.Interfaces;

namespace RTL.TvMazeScraper.SyncProcessor
{
    public class TvMazeJobService(ITvMazeApiService tvMazeApiService, ITvMazeSyncService tvMazeSyncService) : ITvMazeJobService
    {
        public async Task SyncShowsFromApiToDatabaseAsync()
        {
            var shows = await tvMazeApiService.GetShowsFromTvMazeApiAsync();

            await tvMazeSyncService.SyncShowsToDatabaseAsync(shows);
        }
    }
}
