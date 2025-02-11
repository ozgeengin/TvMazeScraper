namespace RTL.TvMazeScraper.Application.Services.Interfaces
{
    public interface IBaseApiService
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}
