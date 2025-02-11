namespace RTL.TvMazeScraper.Application.Models
{
    public record ShowDto(int Id, string Name, IEnumerable<CastPersonDto> Cast) : BaseModelDto(Id, Name)
    {
        public ShowDto() : this(0, string.Empty, []) { }
    }
}
