namespace RTL.TvMazeScraper.Infastructure.Models.Settings
{
    public class RateLimiterSettings
    {
        public int Threshold { set; get; }
        public int TimeFrameInSeconds { set; get; }
        public int MaxBurst { set; get; }
        public int RetryCount { set; get; }
    }
}
