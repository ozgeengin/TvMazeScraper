using RTL.TvMazeScraper.Application.Exceptions;

namespace RTL.TvMazeScraper.Application.Models
{
    public class PaginationRequest
    {
        public int PageIndex { get; }
        public int PageSize { get; }

        public PaginationRequest(int pageIndex, int pageSize)
        {
            if(pageIndex < 1)
            {
                throw new ValueShouldBePositiveException(nameof(pageIndex));
            }

            if(pageSize < 1)
            {
                throw new ValueShouldBePositiveException(nameof(pageSize));
            }

            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
