using RTL.TvMazeScraper.Domain.Common;

namespace RTL.TvMazeScraper.Domain.Entities
{
    public class ShowEntity : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<CastPersonEntity> Cast = [];
    }
}
