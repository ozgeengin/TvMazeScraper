using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Infastructure.Models.Api;

namespace RTL.TvMazeScraper.Infastructure.Services.Interfaces
{
    public interface ITvMazeApiService : IBaseApiService
    {
        Task<List<ShowsApiResponseModel>> GetShowsFromTvMazeApiAsync();
    }
}
