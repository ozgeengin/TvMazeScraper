using RTL.TvMazeScraper.Application.Models;
using RTL.TvMazeScraper.Domain.Entities;
using RTL.TvMazeScraper.Infastructure.Models;

namespace RTL.TvMazeScraper.Infastructure.Mappers
{
    internal class ApiModelMapper
    {
        internal static ShowEntity MapToShowEntity(ShowsApiResponseModel show) => 
            new()
            {
                Id = show.Id,
                Name = show.Name
            };

        internal static CastPersonEntity MapToCastPersonEntity(CastApiResponseModel cast, ShowEntity show) =>
            new()
            {
                Id = cast.Id,
                PersonId = cast.Person.Id,
                Person = new PersonEntity
                {
                    Id = cast.Person.Id,
                    Name = cast.Person.Name,
                    Birthday = cast.Person.Birthday
                },
                Show = show,
                ShowId = show.Id
            };

        internal static ShowDto Map(ShowsApiResponseModel show, IEnumerable<CastApiResponseModel> cast)
        {
            return new ShowDto
            {
                Id = show.Id,
                Name = show.Name,
                Cast = cast.Select(c => new CastPersonDto
                {
                    Id = c.Person.Id,
                    Name = c.Person.Name,
                    Birthday = c.Person.Birthday
                }).ToList()
            };
        }
    }
}
