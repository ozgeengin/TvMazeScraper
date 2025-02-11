using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Application.Repositories
{
    public interface IShowRepository
    {
        Task<IEnumerable<ShowEntity>> GetShowsAsync(int pageIndex, int pageSize);
        Task AddShowsAsync(IEnumerable<ShowEntity> shows);
        Task<IEnumerable<ShowEntity>> FindNewShowsAsync(IEnumerable<ShowEntity> shows);
    }
}
