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

        public static CastPersonEntity MapToCastPersonEntity(CastPersonDto source)
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            return new CastPersonEntity
            {
                Person = new PersonEntity
                {
                    Birthday = source.Birthday,
                    Id = source.Id,
                    Name = source.Name
                },
                PersonId = source.Id,
                Show = null
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        public static CastPersonDto MapToCastPersonDto(CastPersonEntity source)
        {
            return new CastPersonDto
            {
                Birthday = source.Person.Birthday,
                Id = source.Person.Id,
                Name = source.Person.Name
            };
        }
    }
}
