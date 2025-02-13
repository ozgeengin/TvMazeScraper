using RTL.TvMazeScraper.Domain.Models;
using RTL.TvMazeScraper.Domain.Services.Interfaces;

namespace RTL.TvMazeScraper.Domain.Services
{
    public class ShowDomainService : IShowDomainService
    {
        public IOrderedEnumerable<ShowModel> GetOrderedShows(IEnumerable<ShowModel> shows)
        {
            return shows.Select(x => {
                        x.Cast = x.Cast
                            .OrderByDescending(c => c.Birthday)
                            .ToList();

                        return x;
                    })
                .OrderBy(x => x.Id);
        }
    }
}
