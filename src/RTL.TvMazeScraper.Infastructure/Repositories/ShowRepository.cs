using Microsoft.EntityFrameworkCore;
using RTL.TvMazeScraper.Application.Repositories;
using RTL.TvMazeScraper.Domain.Entities;
using RTL.TvMazeScraper.Infastructure.Data;

namespace RTL.TvMazeScraper.Infastructure.Repositories
{
    public class ShowRepository(ApplicationDbContext context) : IShowRepository
    {
        public async Task<IEnumerable<ShowEntity>> GetShowsAsync(int pageNumber, int pageSize)
        {
            return await context.Shows
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddShowsAsync(IEnumerable<ShowEntity> shows)
        {
            foreach (var show in shows)
            {
                var newCast = new List<CastPersonEntity>();
                foreach (var castPerson in show.Cast)
                {
                    var existingPerson = await context.Persons.SingleOrDefaultAsync(x => x.Id == castPerson.Person.Id);
                    if (existingPerson == null)
                    {
                        var newPerson = new PersonEntity()
                        {
                            Name = castPerson.Person.Name
                        };
                        context.Persons.Add(newPerson);

                        var newCastPerson = new CastPersonEntity()
                        {
                            PersonId = newPerson.Id,
                            Person = null!,
                            ShowId = show.Id,
                            Show = null!
                        };
                        context.CastPersons.Add(newCastPerson);
                        newCast.Add(newCastPerson);
                    }
                }
                show.Id = 0;
                show.Cast = newCast;
                context.Shows.Add(show);
            }

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShowEntity>> FindNewShowsAsync(IEnumerable<ShowEntity> shows)
        {
            var result = new List<ShowEntity>();

            foreach (var show in shows)
            {
                var existingShow = await context.Shows.FindAsync(show.Id);
                if (existingShow == null)
                {
                    result.Add(show);
                }
            }

            return result;
        }
    }
}
