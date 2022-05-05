using Priyosaj.Contacts.Entities;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Contacts.Interfaces.Repositories;

public interface IGenericRepository<T> where T : BaseRepositoryItem
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