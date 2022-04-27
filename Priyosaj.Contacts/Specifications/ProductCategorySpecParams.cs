namespace Priyosaj.Contacts.Specifications
{
    public class ProductCategorySpecParams
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
        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}