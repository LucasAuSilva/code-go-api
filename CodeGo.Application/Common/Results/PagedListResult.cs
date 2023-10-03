
namespace CodeGo.Application.Common.Results;

public class PagedListResult<T> {
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public List<T> Data { get; set; }
    public bool HasNextPage => Page * PageSize < TotalPages;
    public bool HasPreviousPage => Page > 1;

    private PagedListResult(List<T> data, int page, int pageSize, int totalPages, int totalRecords)
    {
        Data = data;
        Page = page;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalRecords = totalRecords;
    }

    public static PagedListResult<T> Create(IEnumerable<T> query, int page, int pageSize)
    {
        var totalRecords = query.Count();
        var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var totalPages = ((double)totalRecords / (double)pageSize);
        return new(data, page, pageSize, Convert.ToInt32(Math.Ceiling(totalPages)), totalRecords);
    }

#pragma warning disable CS8618
    public PagedListResult() {}
#pragma warning restore CS8618
}
