using RTL.TvMazeScraper.Domain.Common;

namespace RTL.TvMazeScraper.Domain.Entities
{
    public class PersonEntity : BaseEntity
    {
        public required string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
