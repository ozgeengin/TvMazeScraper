using AutoMapper;
using RTL.TvMazeScraper.Application.Models;
using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Application.UnitTests.Mappers
{
    public static class CustomMapper
    {
        public static ShowEntity MapToShowEntity(ShowDto source)
        {
            return new ShowEntity
            {
                Id = source.Id,
                Cast = source.Cast.Select(MapToCastPersonEntity).ToList(),
                Name = source.Name
            };
        }

        public static ShowDto MapToShowDto(ShowEntity source, IMapper mapper)
        {
            return new ShowDto
            {
                Id = source.Id,
                Cast = mapper.Map<IEnumerable<CastPersonDto>>(source.Cast),
                Name = source.Name
            };
        }

        public static IEnumerable<ShowDto> MapToShowDtos(IEnumerable<ShowEntity> source, IMapper mapper) =>
            source.Select(s => MapToShowDto(s, mapper)).ToList();

        public static CastPersonEntity MapToCastPersonEntity(CastPersonDto source)
        {
            return new CastPersonEntity
            {
                Person = new PersonEntity
                {
                    Birthday = source.Birthday,
                    Id = source.Id,
                    Name = source.Name
                },
                PersonId = source.Id,
                Show = null!
            };
        }

        public static CastPersonDto MapToCastPersonDto(CastPersonEntity source) =>
            new()
            {
                Id = source.Person.Id,
                Name = source.Person.Name,
                Birthday = source.Person.Birthday
            };
    }
}
