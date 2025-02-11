using AutoMapper;
using Bogus;
using RTL.TvMazeScraper.Application.UnitTests.Generators;

namespace RTL.TvMazeScraper.Application.UnitTests
{
    public abstract class BaseTest
    {
        protected IMapper Mapper { get; }
        protected Faker Faker { get; }

        protected BaseTest()
        {
            Mapper = MapperGenerator.Generate();
            Faker = new Faker();
        }
    }
}
