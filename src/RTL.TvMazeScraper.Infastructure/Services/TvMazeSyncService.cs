using AutoMapper;
using RTL.TvMazeScraper.Infastructure.Data;
using RTL.TvMazeScraper.Infastructure.Data.Entities;
using RTL.TvMazeScraper.Infastructure.Models;
using RTL.TvMazeScraper.Infastructure.Services.Interfaces;

namespace RTL.TvMazeScraper.Infastructure.Services
{
    public class TvMazeSyncService(IMapper mapper, ApplicationDbContext context) : ITvMazeSyncService
    {
        public async Task SyncShowsToDatabaseAsync(IEnumerable<ShowsApiResponseModel> shows)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var upcomingPeopleIds = shows.SelectMany(x => x.Cast).Select(x => x.Person.Id).Distinct().ToList();
                var existingPeopleIds = context.People.Where(x => upcomingPeopleIds.Contains(x.TvMazeId)).Select(x => x.TvMazeId).ToList();

                var expiredPeopleIds = existingPeopleIds.Except(upcomingPeopleIds).ToList();
                if (expiredPeopleIds.Any())
                {
                    var peopleToRemove = context.People.Where(x => expiredPeopleIds.Contains(x.TvMazeId)).ToList();
                    context.People.RemoveRange(peopleToRemove);
                    await context.SaveChangesAsync();
                }

                var newPeopleIds = upcomingPeopleIds.Except(existingPeopleIds).ToList();
                if (newPeopleIds.Any())
                {
                    var peopleToAdd = shows.SelectMany(x => x.Cast).Select(x => x.Person).Where(x => newPeopleIds.Contains(x.Id)).DistinctBy(x => x.Id).ToList();
                    var newPeopleEntities = mapper.Map<List<PersonEntity>>(peopleToAdd).ToList();
                    context.People.AddRange(newPeopleEntities);
                    await context.SaveChangesAsync();
                }

                var upcomingShowIds = shows.Select(x => x.Id).Distinct().ToList();
                var existingShowIds = context.Shows.Where(x => upcomingShowIds.Contains(x.TvMazeId)).Select(x => x.TvMazeId).ToList();

                var expiredShowIds = existingShowIds.Except(upcomingShowIds).ToList();
                if (expiredShowIds.Any())
                {
                    var showsToRemove = context.Shows.Where(x => expiredShowIds.Contains(x.TvMazeId)).ToList();
                    context.Shows.RemoveRange(showsToRemove);
                    await context.SaveChangesAsync();
                }

                var newShowIds = upcomingShowIds.Except(existingShowIds).ToList();
                if (newShowIds.Any())
                {
                    var showsToAdd = shows.Where(x => newShowIds.Contains(x.Id)).ToList();
                    var newShowEntities = mapper.Map<List<ShowEntity>>(showsToAdd).ToList();

                    foreach (var show in newShowEntities)
                    {
                        var castIds = show.Cast.Select(x => x.TvMazeId).ToList();
                        show.Cast = context.People.Where(x => castIds.Contains(x.TvMazeId)).ToList();
                    }
                    context.Shows.AddRange(newShowEntities);
                    await context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}