namespace Priyosaj.Api.DTOs.ProductCategoryDTOs;

public class ProductCategoryResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<ProductCategoryResponseDto?> SubCategories { get; set; }
}