using RTL.TvMazeScraper.Application.Exceptions;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Domain.Models;
using RTL.TvMazeScraper.Domain.Services.Interfaces;

namespace RTL.TvMazeScraper.Application.Services
{
    public class ShowService(IShowQueryService showQueryService, IShowDomainService showsDomainService) : IShowService
    {
        public async Task<IOrderedEnumerable<ShowModel>> GetOrderedShowsAsync(int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
            {
                throw new ShouldBeEqualToOrBiggerThanZeroException(nameof(pageIndex));
            }

            if (pageSize < 0)
            {
                throw new ShouldBeEqualToOrBiggerThanZeroException(nameof(pageSize));
            }

            var shows = await showQueryService.GetShowsAsync(pageIndex, pageSize);

            return showsDomainService.GetOrderedShows(shows);
        }
    }
}
