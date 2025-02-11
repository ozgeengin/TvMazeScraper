using RTL.TvMazeScraper.Application.Repositories;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.WebApi.Services.Interfaces;

namespace RTL.TvMazeScraper.WebApi.Services
{
    public class TvMazeJobService(ITvMazeApiService tvMazeApiService, IShowRepository showRepository) : ITvMazeJobService
    {
        public async Task SyncShowsAsync()
        {
            var shows = await tvMazeApiService.GetShowsAsync();
            var newShows = await showRepository.FindNewShowsAsync(shows);

            foreach (var newShow in newShows)
            {
                    var cast = await tvMazeApiService.GetCastAsync(newShow);

                    newShow.Cast = cast;
            }

            await showRepository.AddShowsAsync(newShows);
        }
    }
}
