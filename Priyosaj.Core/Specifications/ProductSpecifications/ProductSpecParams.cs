namespace Priyosaj.Core.Specifications.ProductSpecifications;

public class ProductSpecParams
{
    private const int MaxPageSize = 50;
    
    public int PageIndex { get; set; } = 1;


    private int _pageSize = 6;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Min(MaxPageSize, PageSize);
    }

    public Guid? CategoryId { get; set; }
    
    public string? Sort { get; set; }

    private string? _search;
    public string? Search
    {
        get => _search;
        set => _search = value.ToLower();
    }
}