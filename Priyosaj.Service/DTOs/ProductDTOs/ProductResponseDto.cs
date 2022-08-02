using Priyosaj.Service.DTOs.ProductCategoryDTOs;

namespace Priyosaj.Service.DTOs.ProductDTOs;

public class ProductResponseDto
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }

    public ICollection<ProductCategoryResponseDto> ProductCategories { get; set; } = null!;
}