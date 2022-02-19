using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Interfaces;
using Priyosaj.Contacts.Models;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Business.Data;

public class SpecificationEvaluator<TEntity> where TEntity : BaseRepositoryItem
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}