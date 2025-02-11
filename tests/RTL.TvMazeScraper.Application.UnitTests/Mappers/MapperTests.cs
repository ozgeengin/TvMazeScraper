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
            // Arrange
            var source = FakeDataGenerator.GenerateCastPersonEntity();
            var expected = CustomMapper.MapToCastPersonDto(source);

            // Act
            var result = Mapper.Map<CastPersonDto>(source);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldMapFromCastPersonDtoToCastPersonEntity()
        {
            // Arrange
            var source = FakeDataGenerator.GenerateCastPersonDto();
            var expected = CustomMapper.MapToCastPersonEntity(source);

            // Act
            var result = Mapper.Map<CastPersonEntity>(source);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldMapFromShowEntityToShowDto()
        {
            // Arrange
            var source = FakeDataGenerator.GenerateShowEntity();
            var expected = CustomMapper.MapToShowDto(source, Mapper);

            // Act
            var result = Mapper.Map<ShowDto>(source);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ShouldMapFromShowDtoToShowEntity()
        {
            // Arrange
            var source = FakeDataGenerator.GenerateShowPersonDto();
            var expected = CustomMapper.MapToShowEntity(source);

            // Act
            var result = Mapper.Map<ShowEntity>(source);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
