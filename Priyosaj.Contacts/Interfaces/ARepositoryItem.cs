namespace Priyosaj.Contacts.Interfaces;

public abstract class ARepositoryItem : IRepositoryItem
{
    public virtual Guid GetId()
    {
        return Id;
    }

    public Guid Id;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
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