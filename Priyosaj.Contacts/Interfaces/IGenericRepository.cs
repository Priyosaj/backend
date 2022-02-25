using Priyosaj.Contacts.Models;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Contacts.Interfaces;

public interface IGenericRepository<T> where T : BaseRepositoryItem
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();

    Task<T?> GetEntityWithSpec(ISpecification<T> spec);

    Task<IReadOnlyList<T>> ListAllAsyncWithSpec(ISpecification<T> spec);
    
    Task<int> CountAsync(ISpecification<T> spec);
}