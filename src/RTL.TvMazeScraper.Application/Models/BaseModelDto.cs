namespace RTL.TvMazeScraper.Application.Models
{
    public record BaseModelDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }

        public BaseModelDto() { }

        public BaseModelDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
