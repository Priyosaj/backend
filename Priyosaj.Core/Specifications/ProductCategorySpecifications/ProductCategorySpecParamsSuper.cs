namespace Priyosaj.Core.Specifications.ProductCategorySpecifications;

public class ProductCategorySpecParamsSuper
{
    private const int MaxDepthLevel = 5;
    private int _depthLevel = 3;
    public int DepthLevel
    {
        get => _depthLevel;
        set => _depthLevel = (value > MaxDepthLevel) ? MaxDepthLevel : value;
    }
    public string? Sort { get; set; }
    private string? _search;
    public Boolean? IncludeDeletedItems { get; set; } = false;
    public string? Search
    {
        get => _search;
        set => _search = value.ToLower();
    }
}
