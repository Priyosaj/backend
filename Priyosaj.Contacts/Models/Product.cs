using Priyosaj.Contacts.Interfaces;

namespace Priyosaj.Contacts.Models;

public class Product : ARepositoryItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public override Guid GetId()
    {
        return Id;
    }
}