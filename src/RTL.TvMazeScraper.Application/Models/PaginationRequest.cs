namespace RTL.TvMazeScraper.Application.Models
{
    public class PaginationRequest
    {
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
