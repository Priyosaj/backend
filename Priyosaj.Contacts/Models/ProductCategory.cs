using Priyosaj.Contacts.Interfaces;

namespace Priyosaj.Contacts.Models;

public class ProductCategory : BaseRepositoryItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;

    // public ICollection<Product> Products { get; set; } = null!;
}