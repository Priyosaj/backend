using Priyosaj.Core.Entities;

namespace Priyosaj.Core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : ABaseEntity;
    Task<int> Complete();
}