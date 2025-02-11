using RTL.TvMazeScraper.Domain.Common;

namespace RTL.TvMazeScraper.Domain.Entities
{
    public class CastPersonEntity : BaseEntity
    {
        public int ShowId { get; set; }
        public required ShowEntity Show { get; set; }
        public int PersonId { get; set; }
        public required PersonEntity Person { get; set; }
    }
}
