using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Priyosaj.Contacts.Entities.ProductEntities;

[Index(nameof(Title), IsUnique = true)]
public class ProductCategory : BaseRepositoryItem
{
    // public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid? ParentId { get; set; }
    [JsonIgnore]
    [ForeignKey("ParentId")] public virtual ProductCategory? Parent { get; set; }
    public virtual ICollection<ProductCategory>? SubCategories { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}