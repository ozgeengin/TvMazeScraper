using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Application.Services.Interfaces
{
    public interface ITvMazeApiService : IBaseApiService
    {
        Task<IEnumerable<ShowEntity>> GetShowsAsync();
        Task<ICollection<CastPersonEntity>> GetCastAsync(ShowEntity showEntity);
    }
}
