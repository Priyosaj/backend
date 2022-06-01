using Priyosaj.Contacts.Entities.ProductEntities;

namespace Priyosaj.Api.DTOs.ProductDTOs;

public class ProductResponseDto
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; } = null!;
}