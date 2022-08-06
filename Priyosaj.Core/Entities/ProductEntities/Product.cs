using System.ComponentModel.DataAnnotations.Schema;
using Priyosaj.Core.Entities.IdentityEntities;
using Priyosaj.Core.Entities.PromotionalEventEntities;

namespace Priyosaj.Core.Entities.ProductEntities;

public class Product : ABaseEntity
{
    public string Title { get; set; } = string.Empty;
    
    // public ICollection<>

    public string Description { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int StockCount { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }


    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public ICollection<PromotionalEventProductMapping> PromotionalEventProductMappings { get; set; } =
        new List<PromotionalEventProductMapping>();
}