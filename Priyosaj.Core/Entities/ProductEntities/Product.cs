using Priyosaj.Core.Entities.PromotionalEventEntities;

namespace Priyosaj.Core.Entities.ProductEntities;

public class Product : ABaseEntity
{
    // public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }
    public decimal? DiscountPrice { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; } = null!;
    public ICollection<PromotionalEventProductMapping> PromotionalEventProductMappings { get; set; }
}