namespace RTL.TvMazeScraper.Infastructure.Settings
{
    public class TvMazeApiSettings
    {
        public string Api1Url { get; set; }
        public string Api2Url { get; set; }
    }

    public static class RateLimiterSettings
    {
        public static int Threshold => 100;
        public static int TimeFrameInSeconds => 60;
        public static int MaxBurst => 12;
    }

    public static class HttpClientPolicy
    {
        public static int RetryTime => 7;
        public static int BeforeBreakCount => 7;
        public static int CircuitBreakerDurationOnBreakInSeconds => 5;
    }
}
