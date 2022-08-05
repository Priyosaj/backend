using Priyosaj.Core.Entities;

namespace Priyosaj.Core.Interfaces.Repositories;

public interface IGenericRepository<T> where T : ABaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();

    Task<T?> GetEntityWithSpec(ISpecification<T> spec);

    Task<IReadOnlyList<T>> ListAllAsyncWithSpec(ISpecification<T> spec);
    
    Task<int> CountAsync(ISpecification<T> spec);
    
    void Add(T entity);
    
    void Update(T entity);
    
    void Delete(T entity);
}