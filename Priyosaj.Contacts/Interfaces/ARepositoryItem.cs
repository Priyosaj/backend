namespace Priyosaj.Contacts.Interfaces;

public abstract class ARepositoryItem : IRepositoryItem
{
    public abstract Guid GetId();
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}

/*
 public ARepositoryItem()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }
    public abstract string GetId();
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
*/