namespace Priyosaj.Api.DTOs
{
    public class ProductCategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<ProductCategoryResponseDto?> SubCategories { get; set; }
    }
}