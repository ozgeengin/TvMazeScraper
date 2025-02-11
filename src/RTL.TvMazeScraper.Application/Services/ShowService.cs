using AutoMapper;
using RTL.TvMazeScraper.Application.Models;
using RTL.TvMazeScraper.Application.Repositories;
using RTL.TvMazeScraper.Application.Services.Interfaces;

namespace RTL.TvMazeScraper.Application.Services
{
    public class ShowService(IShowRepository showRepository, IMapper mapper) : IShowService
    {
        public async Task<PaginatedList<ShowDto>> GetShowsAsync(PaginationRequest paginationRequest)
        {
            var showEntities = await showRepository.GetShowsAsync(paginationRequest.PageIndex, paginationRequest.PageSize);

            var showDtos = mapper.Map<IEnumerable<ShowDto>>(showEntities);

            return new PaginatedList<ShowDto>(paginationRequest.PageIndex, paginationRequest.PageSize, showDtos);
        }
    }
}
