using Microsoft.AspNetCore.Mvc;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Domain.Models;

namespace RTL.TvMazeScraper.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class ShowController(IShowService showService) : ControllerBase
    {
        [HttpGet]
        [Route("shows")]
        [ProducesResponseType<IAsyncEnumerable<ShowModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderedShowsAsync(int pageIndex, int pageSize)
        {
            return Ok(await showService.GetOrderedShowsAsync(pageIndex, pageSize));
        }
    }
}