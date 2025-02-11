using AutoMapper;
using RTL.TvMazeScraper.Application.UnitTests.Generators;

namespace RTL.TvMazeScraper.Application.UnitTests
{
    public abstract class BaseTest
    {
        protected IMapper Mapper { get; }

        protected BaseTest()
        {
            Mapper = MapperGenerator.Generate();
        }
    }
}
