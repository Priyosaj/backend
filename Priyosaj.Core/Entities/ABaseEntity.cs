using Priyosaj.Core.Interfaces.Repositories;

namespace Priyosaj.Core.Entities;

public abstract class ABaseEntity
{
    // public virtual Guid GetId()
    // {
    //     return Id;
    // }

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; } = null;
}