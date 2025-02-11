using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Application.Repositories
{
    public interface IShowRepository
    {
        Task<IEnumerable<ShowEntity>> GetShowsAsync(int pageNumber, int pageSize);
    }
}
