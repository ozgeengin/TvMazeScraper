using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RTL.TvMazeScraper.WebApi.IntegrationTests
{
    public class ShowsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private const string apiRoute = "/api/v1/shows";

        [Fact]
        public async Task GetOrderedShowsReturnsStatusCode200()
        {
            // Arrange
            var client = new WebApplicationFactory<Program>().CreateClient();

            // Act
            var response = await client.GetAsync($"{apiRoute}?pageIndex=1&pageSize=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetOrderedShowsReturnsStatusCode400()
        {
            // Arrange
            var client = new WebApplicationFactory<Program>().CreateClient();

            // Act
            var response = await client.GetAsync($"{apiRoute}?pageIndex=-1&pageSize=-1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetOrderedShowsReturnsStatusCode404()
        {
            // Arrange
            var client = new WebApplicationFactory<Program>().CreateClient();

            // Act
            var response = await client.GetAsync($"{apiRoute}/show");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
