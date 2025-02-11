using Microsoft.Extensions.Options;
using Polly;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Infastructure.Mappers;
using RTL.TvMazeScraper.Infastructure.Models;
using RTL.TvMazeScraper.Infastructure.Settings;
using RTL.TvMazeScraper.Infastructure.Helpers;
using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Infastructure.Services
{
    internal class TvMazeApiService : BaseApiService, ITvMazeApiService
    {
        private readonly IOptions<TvMazeApiSettings> apiSettings;

        public TvMazeApiService(
        IOptions<TvMazeApiSettings> apiSettings,
        IHttpClientFactory httpClientFactory,
        IAsyncPolicy rateLimiter) : base(httpClientFactory, rateLimiter)
        {
            this.apiSettings = apiSettings;
        }

        public async Task<IEnumerable<ShowEntity>> GetShowsAsync()
        {
            var response = await GetAsync<IEnumerable<ShowsApiResponseModel>>(apiSettings.Value.ShowsApiUrl);

            return response.Select(ApiModelMapper.MapToShowEntity);
        }

        public async Task<ICollection<CastPersonEntity>> GetCastAsync(ShowEntity showEntity)
        {
            var castApiUrl = UrlHelper.GetCastApiUrl(
                apiSettings.Value.CastApiUrl,
                apiSettings.Value.CastApiUrlPlaceholder,
                showEntity.Id);

            var response = await GetAsync<IEnumerable<CastApiResponseModel>>(castApiUrl);

            return response.Select(x => ApiModelMapper.MapToCastPersonEntity(x, showEntity)).ToList();
        }
    }
}
