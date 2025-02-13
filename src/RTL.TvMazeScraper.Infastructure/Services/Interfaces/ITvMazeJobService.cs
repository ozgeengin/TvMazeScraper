namespace RTL.TvMazeScraper.Infastructure.Services.Interfaces
{
    public interface ITvMazeJobService
    {
        Task SyncShowsFromApiToDatabaseAsync();
    }
}
