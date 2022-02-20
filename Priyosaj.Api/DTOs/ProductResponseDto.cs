using Priyosaj.Contacts.Models;

namespace Priyosaj.Api.DTOs;

public class ProductResponseDto
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<ProductCategory> ProductCategories { get; set; } = null!;
}