using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Priyosaj.Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    // List<Expression<Func<T, object>>> Includes { get; }
    List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    Expression<Func<T, object>>? GroupBy { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}