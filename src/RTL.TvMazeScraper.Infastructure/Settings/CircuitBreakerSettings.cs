namespace RTL.TvMazeScraper.Infastructure.Settings
{
    public class CircuitBreakerSettings
    {
        public int RetryTime { set; get; }
        public int HandledEventsAllowedBeforeBreaking { set; get; }
        public int CircuitBreakerDurationOnBreakInSeconds { set; get; }
    }
}
