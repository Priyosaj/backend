namespace Priyosaj.Core.Models;

public class ApiPaginatedResponse<T>: ApiResponse where T : class
{
    public ApiPaginatedResponse(int pageIndex, int pageSize, int count, IReadOnlyList<T> data) : base(200, "Paginated Data Retrieved.")
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
        IsPaginated = true;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IReadOnlyList<T> Data { get; set; }
}