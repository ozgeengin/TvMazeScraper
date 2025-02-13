using RTL.TvMazeScraper.Domain.Models;

namespace RTL.TvMazeScraper.Application.Services.Interfaces
{
    public interface IShowQueryService
    {
        Task<IEnumerable<ShowModel>> GetShowsAsync(int pageIndex, int pageSize);
    }
}
