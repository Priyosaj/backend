using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Interfaces;
using Priyosaj.Contacts.Models;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Business.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseRepositoryItem
{
    private readonly StoreContext _context;

    public GenericRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllAsyncWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
}