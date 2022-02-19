using Priyosaj.Contacts.Interfaces;

namespace Priyosaj.Contacts.Models;

public class Product : ARepositoryItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
}