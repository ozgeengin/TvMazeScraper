using RTL.TvMazeScraper.Application.Repositories;
using RTL.TvMazeScraper.Domain.Entities;
using RTL.TvMazeScraper.Infastructure.Data;

namespace RTL.TvMazeScraper.Infastructure.Repositories
{
    public class ShowRepository(ApplicationDbContext context) : IShowRepository
    {
        public async Task<IEnumerable<ShowEntity>> GetShowsAsync(int pageNumber, int pageSize)
        {
            return context.Shows.Skip(pageNumber * pageSize).Take(pageSize);
        }
    }
}
