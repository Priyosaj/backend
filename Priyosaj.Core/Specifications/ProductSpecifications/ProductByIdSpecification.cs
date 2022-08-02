using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.ProductEntities;

namespace Priyosaj.Core.Specifications.ProductSpecifications;

public class ProductByIdSpecification : BaseSpecification<Product>
{
    public ProductByIdSpecification(Guid id) : base(p => p.Id == id)
    {
        AddInclude(x => x.Include(p => p.ProductCategories));
    }
}