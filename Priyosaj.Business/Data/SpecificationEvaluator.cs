using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Entities;
using Priyosaj.Contacts.Interfaces;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Business.Data;

public static class SpecificationEvaluator<TEntity> where TEntity : BaseRepositoryItem
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        query = spec.Includes.Aggregate(query, (current, include) => include(current));
        
        return query;
    }
}