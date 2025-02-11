namespace RTL.TvMazeScraper.Infastructure.Helpers
{
    public static class UrlHelper
    {
        public static string GetCastApiUrl(string castApiUrl, string castApiUrlPlaceholder, int showId)
        {
            return castApiUrl.Replace(castApiUrlPlaceholder, $"{showId}");
        }
    }
}
