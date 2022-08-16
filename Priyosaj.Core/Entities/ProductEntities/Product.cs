using System.ComponentModel.DataAnnotations.Schema;
using Priyosaj.Core.Entities.IdentityEntities;
using Priyosaj.Core.Entities.PromotionalEventEntities;

namespace Priyosaj.Core.Entities.ProductEntities;

public class Product : ABaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Specifications { get; set; } = "[]";
    public decimal RegularPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int StockCount { get; set; }
    public Guid CreatedById { get; set; }
    public AppUser CreatedBy { get; set; }
    public Guid? DisplayImageId { get; set; } = null;
    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    public ICollection<FileEntity> Images { get; set; } = new List<FileEntity>();

    public ICollection<PromotionalEventProductMapping> PromotionalEventProductMappings { get; set; } =
        new List<PromotionalEventProductMapping>();
}

public class Specification
{
    public string Title { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}