namespace RTL.TvMazeScraper.Application.Exceptions
{
    public class ValueShouldBePositiveException : Exception
    {
        public ValueShouldBePositiveException()
        {
        }

        public ValueShouldBePositiveException(string? propertyName) : base($"{propertyName} should be positive")
        {
        }
    }
}
