using Microsoft.Extensions.Options;
using Polly;
using RTL.TvMazeScraper.Infastructure.Helpers;
using RTL.TvMazeScraper.Infastructure.Services.Interfaces;
using RTL.TvMazeScraper.Infastructure.Models.Settings;
using RTL.TvMazeScraper.Infastructure.Models.Api;

namespace RTL.TvMazeScraper.Infastructure.Services
{
    public class TvMazeApiService : BaseApiService, ITvMazeApiService
    {
        private readonly IOptions<TvMazeApiSettings> apiSettings;

        public TvMazeApiService(
        IOptions<TvMazeApiSettings> apiSettings,
        IHttpClientFactory httpClientFactory,
        IAsyncPolicy rateLimiter) : base(httpClientFactory, rateLimiter)
        {
            this.apiSettings = apiSettings;
        }

        public async Task<List<ShowsApiResponseModel>> GetShowsFromTvMazeApiAsync()
        {
            var shows = await GetAsync<List<ShowsApiResponseModel>>(apiSettings.Value.ShowsApiUrl);

            foreach (var show in shows)
            {
                var cast = await GetCastAsync(show.Id);
                show.Cast = cast.Distinct().ToList();
            }

            return shows;
        }

        private async Task<List<CastApiResponseModel>> GetCastAsync(int showId)
        {
            var castApiUrl = UrlHelper.GetCastApiUrl(
                apiSettings.Value.CastApiUrl,
                apiSettings.Value.CastApiUrlPlaceholder,
                showId);

            return await GetAsync<List<CastApiResponseModel>>(castApiUrl);
        }
    }
}
