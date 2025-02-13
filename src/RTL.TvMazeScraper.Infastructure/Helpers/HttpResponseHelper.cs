using Newtonsoft.Json;

namespace RTL.TvMazeScraper.Infastructure.Helpers
{
    public static class HttpResponseHelper
    {
        public static async Task<T> GetContentAsync<T>(HttpResponseMessage message)
        {
            var responseContent = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseContent)!;
        }
    }
}
