using Priyosaj.Contacts.Interfaces;

namespace Priyosaj.Contacts.Models;

public class BaseRepositoryItem : IRepositoryItem
{
    public virtual Guid GetId()
    {
        return Id;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
}