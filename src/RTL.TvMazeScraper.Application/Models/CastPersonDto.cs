namespace RTL.TvMazeScraper.Application.Models
{
    // todo check if bithday is a string on response
    public record CastPersonDto(int Id, string Name, DateTime Birthday) : BaseModelDto(Id, Name)
    {
        public CastPersonDto() : this(0, string.Empty, DateTime.MinValue) { }
    }
}
