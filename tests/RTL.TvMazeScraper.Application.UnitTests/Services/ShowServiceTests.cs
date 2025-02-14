using FluentAssertions;
using Moq;
using RTL.TvMazeScraper.Application.Exceptions;
using RTL.TvMazeScraper.Application.Services;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Application.UnitTests.Helpers;
using RTL.TvMazeScraper.Domain.Services;
using RTL.TvMazeScraper.Domain.Services.Interfaces;

namespace RTL.TvMazeScraper.Application.UnitTests.Services
{
    public class ShowServiceTests
    {
        private readonly Mock<IShowQueryService> showQueryServiceMock;
        private readonly IShowDomainService showDomainService;

        public ShowServiceTests()
        {
            showQueryServiceMock = new Mock<IShowQueryService>();
            showDomainService = new ShowDomainService();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 6)]
        [InlineData(2, 20)]
        public async Task ShouldReturnExpectedShowsWithCorrectOrder(int pageIndex, int pageSize)
        {
            showQueryServiceMock
                .Setup(x => x.GetShowsAsync(pageIndex, pageSize))
                .ReturnsAsync(DataGenerator.GenerateShows(pageSize));

            var sut = new ShowService(showQueryServiceMock.Object, showDomainService);

            // Act
            var result = await sut.GetOrderedShowsAsync(pageIndex, pageSize);

            // Assert
            result.Should().NotBeEmpty()
                .And.NotContainNulls()
                .And.HaveCount(pageSize)
                .And.OnlyHaveUniqueItems()
                .And.BeInAscendingOrder(s => s.Id)
                .And.AllSatisfy(x => x.Cast.Should().BeInDescendingOrder(p => p.Birthday));
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-1, -1)]
        public async Task ShouldThrowShouldBeEqualToOrBiggerThanZeroException(int pageIndex, int pageSize)
        {
            // Arrange
            var sut = new ShowService(showQueryServiceMock.Object, showDomainService);

            // Act
            var action = () => sut.GetOrderedShowsAsync(pageIndex, pageSize);

            // Assert
            await action.Should().ThrowAsync<ShouldBeEqualToOrBiggerThanZeroException>();
        }
    }
}
