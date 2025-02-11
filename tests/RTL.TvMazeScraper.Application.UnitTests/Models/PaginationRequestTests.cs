using Bogus;
using FluentAssertions;
using RTL.TvMazeScraper.Application.Exceptions;
using RTL.TvMazeScraper.Application.Models;

namespace RTL.TvMazeScraper.Application.UnitTests.Models
{
    public class PaginationRequestTests : BaseTest
    {
        [Fact]
        public void ShouldThrowValueShouldBePositiveExceptionForPageIndex()
        {
            // Arrange            
            var pageIndex = GenerateNegativeNumber();
            var pageSize = GeneratePositiveNumber();

            // Act
            var action = () => new PaginationRequest(pageIndex, pageSize);

            // Assert
            action.Should().Throw<ValueShouldBePositiveException>(nameof(pageIndex));
        }

        [Fact]
        public void ShouldThrowValueShouldBePositiveExceptionForPageSize()
        {
            // Arrange
            var pageIndex = GeneratePositiveNumber();
            var pageSize = GenerateNegativeNumber();

            // Act
            var action = () => new PaginationRequest(pageIndex, pageSize);

            // Assert
            action.Should().Throw<ValueShouldBePositiveException>(nameof(pageSize));
        }

        [Fact]
        public void ShouldNotThrowValueShouldBePositiveException()
        {
            // Arrange
            var pageIndex = GeneratePositiveNumber();
            var pageSize = GeneratePositiveNumber();

            // Act
            var action = () => new PaginationRequest(pageIndex, pageSize);

            // Assert
            action.Should().NotThrow<ValueShouldBePositiveException>();
        }

        private int GeneratePositiveNumber()
        {
            return Faker.Random.Number(1, int.MaxValue);
        }

        private int GenerateNegativeNumber()
        {
            return Faker.Random.Number(int.MinValue, -1);
        }
    }
}
