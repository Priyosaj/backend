using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Interfaces;

namespace Priyosaj.Data.Specifications.ProductSpecifications;

public class ProductByIdSpecification : ABaseSpecification<Product>
{
    public ProductByIdSpecification(Guid id) : base(p => p.Id == id)
    {
        AddInclude(x => x.Include(p => p.ProductCategories));
        AddInclude(x => x.Include(p => p.Images));
        AddInclude(x => x.Include(p => p.CreatedBy));
    }
}