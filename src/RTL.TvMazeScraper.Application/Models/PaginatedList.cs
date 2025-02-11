namespace RTL.TvMazeScraper.Application.Models
{
    public class PaginatedList<T>(int pageIndex, int pageSize, IEnumerable<T> data)
    {
        public int PageIndex { get; private set; } = pageIndex;

        public int PageSize { get; private set; } = pageSize;

        public long Count { get; private set; } = data.Count();

        public IEnumerable<T> Data { get; private set; } = data;
    }
}
