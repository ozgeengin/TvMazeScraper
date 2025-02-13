using RTL.TvMazeScraper.Domain.Models;

namespace RTL.TvMazeScraper.Domain.Services.Interfaces
{
    public interface IShowDomainService
    {
        IOrderedEnumerable<ShowModel> GetOrderedShows(IEnumerable<ShowModel> shows);
    }
}
