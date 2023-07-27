namespace DeepIn.Application.Models
{
    public interface IPagedResult
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
    }
    public interface IPagedResult<T> : IPagedResult
        where T : class
    {
        IEnumerable<T> Items { get; }
    }
}
