using Priyosaj.Contacts.Entities;

namespace Priyosaj.Contacts.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseRepositoryItem;
    Task<int> Complete();
}