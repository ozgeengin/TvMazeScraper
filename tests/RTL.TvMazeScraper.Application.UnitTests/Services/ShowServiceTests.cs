using FluentAssertions;
using Moq;
using RTL.TvMazeScraper.Application.Exceptions;
using RTL.TvMazeScraper.Application.Models;
using RTL.TvMazeScraper.Application.Repositories;
using RTL.TvMazeScraper.Application.Services;
using RTL.TvMazeScraper.Application.UnitTests.Generators;
using RTL.TvMazeScraper.Application.UnitTests.Mappers;

namespace RTL.TvMazeScraper.Application.UnitTests.Services
{
    public class ShowServiceTests : BaseTest
    {
        private readonly Mock<IShowRepository> ShowRepositoryMock;

        public ShowServiceTests()
        {
            ShowRepositoryMock = new Mock<IShowRepository>();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 6)]
        [InlineData(2, 20)]
        public async Task ShouldReturnExpectedShows(int pageIndex, int pageSize)
        {
            // Arrange
            var expected = GenerateExpected(pageIndex, pageSize);
            var sut = new ShowService(ShowRepositoryMock.Object, Mapper);

            // Act
            var result = await sut.GetShowsAsync(new PaginationRequest(pageIndex, pageSize));

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(0, 4)]
        [InlineData(0, 0)]
        public async Task ShouldThrowValueShouldBePositiveException(int pageIndex, int pageSize)
        {
            // Arrange
            var sut = new ShowService(ShowRepositoryMock.Object, Mapper);

            // Act
            var action = () => sut.GetShowsAsync(new PaginationRequest(pageIndex, pageSize));

            // Assert
            await action.Should().ThrowAsync<ValueShouldBePositiveException>();
        }

        private PaginatedList<ShowDto> GenerateExpected(int pageIndex, int pageSize)
        {
            var showEntities = FakeDataGenerator.GenerateShowEntities(pageSize).ToList();
            ShowRepositoryMock.Setup(x => x.GetShowsAsync(pageIndex, pageSize)).ReturnsAsync(showEntities);
            var showDtos = CustomMapper.MapToShowDtos(showEntities, Mapper);
            
            return new PaginatedList<ShowDto>(pageIndex, pageSize, showDtos);
        }
    }
}
