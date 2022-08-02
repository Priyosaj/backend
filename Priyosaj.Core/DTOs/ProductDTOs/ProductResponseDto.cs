using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.ProductDTOs;

public class ProductResponseDto : IMapFrom<Product>
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }

    public ICollection<ProductCategoryResponseDto> ProductCategories { get; set; } = null!;
}