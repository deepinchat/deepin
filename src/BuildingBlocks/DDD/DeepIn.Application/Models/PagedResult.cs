namespace DeepIn.Application.Models
{
    public class PagedResult<T> : IPagedResult<T> where T : class
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public IEnumerable<T> Items { get; private set; }
        public PagedResult(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalCount / pageSize;
            if (totalCount % pageSize > 0)
                TotalPages++;
        }
        public PagedResult(IEnumerable<T> items, IPagedResult paged)
        {
            Items = items;
            PageIndex = paged.PageIndex;
            PageSize = paged.PageSize;
            TotalCount = paged.TotalCount;
            TotalPages = paged.TotalPages;
        }
        public PagedResult(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex > 0)
                pageIndex--;
            int totalCount = source.Count();
            TotalCount = totalCount;
            TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                TotalPages++;
            PageSize = pageSize;
            PageIndex = pageIndex + 1;
            Items = source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public PagedResult(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex > 0)
                pageIndex--;
            int totalCount = source.Count();
            TotalCount = totalCount;
            TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                TotalPages++;
            PageSize = pageSize;
            PageIndex = pageIndex + 1;
            Items = source.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}
