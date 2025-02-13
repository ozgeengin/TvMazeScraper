using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Polly;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Domain.Models;
using RTL.TvMazeScraper.Infastructure.Data;

namespace RTL.TvMazeScraper.Infastructure.Services
{
    public class ShowQueryService(ApplicationDbContext context, IMapper mapper) : IShowQueryService
    {
        public async Task<IEnumerable<ShowModel>> GetShowsAsync(int pageIndex, int pageSize)
        {
            var showEntities = await context.Shows
                .Include(x => x.Cast)
                .OrderBy(x => x.TvMazeId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return mapper.Map<IEnumerable<ShowModel>>(showEntities);
        }
    }
}
