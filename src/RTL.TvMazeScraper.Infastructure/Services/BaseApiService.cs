using Polly;
using RTL.TvMazeScraper.Application.Services.Interfaces;
using RTL.TvMazeScraper.Infastructure.Helpers;

namespace RTL.TvMazeScraper.Infastructure.Services
{
    public abstract class BaseApiService(IHttpClientFactory httpClientFactory, IAsyncPolicy rateLimiter) : IBaseApiService
    {
        public async Task<T> GetAsync<T>(string requestUri)
        {
            return await rateLimiter.ExecuteAsync(() => GetRequestAsync<T>(requestUri));
        }

        private async Task<T> GetRequestAsync<T>(string requestUri)
        {
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            return await HttpResponseHelper.GetContentAsync<T>(response);
        }
    }
}
