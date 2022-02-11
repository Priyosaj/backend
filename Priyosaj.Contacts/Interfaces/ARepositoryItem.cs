namespace Priyosaj.Contacts.Interfaces;

public abstract class ARepositoryItem : IRepositoryItem
{
    public abstract Guid GetId();
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}