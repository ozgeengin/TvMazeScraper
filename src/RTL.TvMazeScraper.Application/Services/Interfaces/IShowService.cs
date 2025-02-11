using RTL.TvMazeScraper.Application.Models;

namespace RTL.TvMazeScraper.Application.Services.Interfaces
{
    public interface IShowService
    {
        Task<PaginatedList<ShowDto>> GetShowsAsync(PaginationRequest paginationRequest);
    }
}
