using Priyosaj.Contacts.Interfaces;

namespace Priyosaj.Contacts.Models;

public class Product : BaseRepositoryItem
{
    // public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }
    public decimal DiscountPrice { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; } = null!;
    // public Guid ProductCategoryId { get; set; }
}