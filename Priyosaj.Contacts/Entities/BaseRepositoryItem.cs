﻿using Priyosaj.Contacts.Interfaces.Repositories;

namespace Priyosaj.Contacts.Entities;

public class BaseRepositoryItem : IRepositoryItem
{
    public virtual Guid GetId()
    {
        return Id;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; } = null;
}