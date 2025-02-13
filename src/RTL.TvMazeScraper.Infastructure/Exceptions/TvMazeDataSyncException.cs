namespace RTL.TvMazeScraper.Infastructure.Exceptions
{
    public class TvMazeDataSyncException(string message, Exception? innerException) : Exception(message, innerException) { }
}
