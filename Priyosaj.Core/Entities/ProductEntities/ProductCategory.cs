using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Priyosaj.Core.Entities.ProductEntities;

[Index(nameof(Title), IsUnique = true)]
public class ProductCategory : ABaseEntity
{
    public string Title { get; set; }
    public Guid? ParentId { get; set; }
    [JsonIgnore]
    [ForeignKey("ParentId")] public virtual ProductCategory? Parent { get; set; }
    public virtual ICollection<ProductCategory>? SubCategories { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}