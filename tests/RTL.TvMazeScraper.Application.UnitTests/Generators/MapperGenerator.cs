using AutoMapper;
using RTL.TvMazeScraper.Application.Mappers;

namespace RTL.TvMazeScraper.Application.UnitTests.Generators
{
    public static class MapperGenerator
    {
        public static IMapper Generate()
        {
            var configuration = new MapperConfiguration(config =>
                config.AddProfile<ApplicationProfile>());

            return configuration.CreateMapper();
        }
    }
}
