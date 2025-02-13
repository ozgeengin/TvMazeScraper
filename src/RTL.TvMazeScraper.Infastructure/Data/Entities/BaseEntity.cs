namespace RTL.TvMazeScraper.Infastructure.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int TvMazeId { get; set; }
    }
}
