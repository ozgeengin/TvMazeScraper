using AutoMapper;
using RTL.TvMazeScraper.Infastructure.Data;
using RTL.TvMazeScraper.Infastructure.Data.Entities;
using RTL.TvMazeScraper.Infastructure.Exceptions;
using RTL.TvMazeScraper.Infastructure.Models.Api;
using RTL.TvMazeScraper.Infastructure.Services.Interfaces;

namespace RTL.TvMazeScraper.Infastructure.Services
{
    public class TvMazeSyncService : ITvMazeSyncService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public TvMazeSyncService(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task SyncShowsToDatabaseAsync(IEnumerable<ShowsApiResponseModel> shows)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await SyncPeopleToDatabase(shows);

                await SyncShowsToDatabase(shows);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw new TvMazeDataSyncException(ex.Message, ex.InnerException);
            }
        }

        private async Task SyncPeopleToDatabase(IEnumerable<ShowsApiResponseModel> shows)
        {
            var upcomingPeopleIds = shows.SelectMany(x => x.Cast)
                .Select(x => x.Person.Id)
                .Distinct()
                .ToList();

            var existingPeopleIds = _context.People
                .Where(x => upcomingPeopleIds.Contains(x.TvMazeId))
                .Select(x => x.TvMazeId)
                .ToList();

            var oldPeopleIds = existingPeopleIds.Except(upcomingPeopleIds).ToList();
            if (oldPeopleIds.Count != 0)
            {
                await DeleteOldPeople(oldPeopleIds);
            }

            var newPeopleIds = upcomingPeopleIds.Except(existingPeopleIds).ToList();
            if (newPeopleIds.Count != 0)
            {
                await InsertNewPeople(shows, newPeopleIds);
            }
        }

        private async Task DeleteOldPeople(List<int> oldPeopleIds)
        {
            var peopleToRemove = _context.People.Where(x => oldPeopleIds.Contains(x.TvMazeId)).ToList();
            _context.People.RemoveRange(peopleToRemove);
            await _context.SaveChangesAsync();
        }

        private async Task InsertNewPeople(IEnumerable<ShowsApiResponseModel> shows, List<int> newPeopleIds)
        {
            var peopleToAdd = shows.SelectMany(x => x.Cast)
                .Select(x => x.Person)
                .Where(x => newPeopleIds.Contains(x.Id))
                .DistinctBy(x => x.Id)
                .ToList();

            var newPeopleEntities = _mapper.Map<List<PersonEntity>>(peopleToAdd).ToList();

            foreach (var person in newPeopleEntities)
            {
                var showIds = shows
                    .Where(x => x.Cast.Any(c => c.Person.Id == person.TvMazeId))
                    .Select(x => x.Id)
                    .ToList();

                // Assigning the shows here so that ef will create ShowCast entry for the
                // (ShowTvMazeId, PersonTvMazeId) pair and won't throw an exception
                // when same shows are added for different people in AddRange function.
                person.Shows = _context.Shows.Where(x => showIds.Contains(x.TvMazeId)).ToList();
            }

            _context.People.AddRange(newPeopleEntities);
            await _context.SaveChangesAsync();
        }

        private async Task SyncShowsToDatabase(IEnumerable<ShowsApiResponseModel> shows)
        {
            var upcomingShowIds = shows.Select(x => x.Id).Distinct().ToList();
            var existingShowIds = _context.Shows
                .Where(x => upcomingShowIds.Contains(x.TvMazeId))
                .Select(x => x.TvMazeId)
                .ToList();

            var oldShowIds = existingShowIds.Except(upcomingShowIds).ToList();
            if (oldShowIds.Count != 0)
            {
                await DeleteOldShows(oldShowIds);
            }

            var newShowIds = upcomingShowIds.Except(existingShowIds).ToList();
            if (newShowIds.Count != 0)
            {
                await InsertNewShows(shows, newShowIds);
            }
        }

        private async Task DeleteOldShows(List<int> oldShowIds)
        {
            var showsToRemove = _context.Shows.Where(x => oldShowIds.Contains(x.TvMazeId)).ToList();
            _context.Shows.RemoveRange(showsToRemove);
            await _context.SaveChangesAsync();
        }

        private async Task InsertNewShows(IEnumerable<ShowsApiResponseModel> shows, List<int> newShowIds)
        {
            var showsToAdd = shows.Where(x => newShowIds.Contains(x.Id)).ToList();
            var newShowEntities = _mapper.Map<List<ShowEntity>>(showsToAdd).ToList();

            foreach (var show in newShowEntities)
            {
                var castIds = show.Cast.Select(x => x.TvMazeId).ToList();

                // Assigning the people here so that ef will create ShowCast entry for the
                // (ShowTvMazeId, PersonTvMazeId) pair and won't throw an exception
                // when same people are added for different shows in AddRange function.
                show.Cast = _context.People.Where(x => castIds.Contains(x.TvMazeId)).ToList();
            }
            _context.Shows.AddRange(newShowEntities);
            await _context.SaveChangesAsync();
        }
    }
}