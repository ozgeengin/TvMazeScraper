namespace RTL.TvMazeScraper.Application.Exceptions
{
    public class ShouldBeEqualToOrBiggerThanZeroException : Exception
    {
        public ShouldBeEqualToOrBiggerThanZeroException()
        {
        }

        public ShouldBeEqualToOrBiggerThanZeroException(string? propertyName) : base($"{propertyName} should be equal to or bigger than zero.")
        {
        }
    }
}
