using RTL.TvMazeScraper.Domain.Models;

namespace RTL.TvMazeScraper.Application.Services.Interfaces
{
    public interface IShowService
    {
        Task<IOrderedEnumerable<ShowModel>> GetOrderedShowsAsync(int pageIndex, int pageSize);
    }
}
