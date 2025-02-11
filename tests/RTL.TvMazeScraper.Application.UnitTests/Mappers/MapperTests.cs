using FluentAssertions;
using RTL.TvMazeScraper.Application.Models;
using RTL.TvMazeScraper.Application.UnitTests.Generators;
using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Application.UnitTests.Mappers
{
    public class MapperTests : BaseTest
    {
        [Fact]
        public void ShouldMapFromCastPersonEntityToCastPersonDto()
        {
            var source = FakeDataGenerator.GenerateCastPersonEntity();
            var expected = CustomMapper.MapToCastPersonDto(source);

            var result = Mapper.Map<CastPersonDto>(source);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldMapFromCastPersonDtoToCastPersonEntity()
        {
            var source = FakeDataGenerator.GenerateCastPersonDto();
            var expected = CustomMapper.MapToCastPersonEntity(source);

            var result = Mapper.Map<CastPersonEntity>(source);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldMapFromShowEntityToShowDto()
        {
            var source = FakeDataGenerator.GenerateShowEntity();
            var expected = CustomMapper.MapToShowDto(source, Mapper);

            var result = Mapper.Map<ShowDto>(source);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldMapFromShowDtoToShowEntity()
        {
            var source = FakeDataGenerator.GenerateShowPersonDto();
            var expected = CustomMapper.MapToShowEntity(source);

            var result = Mapper.Map<ShowEntity>(source);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
