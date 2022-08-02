using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.ProductEntities;

namespace Priyosaj.Core.Specifications.ProductCategorySpecifications;

public class ProductCategoryByIdSpecification: BaseSpecification<ProductCategory>
{
    public ProductCategoryByIdSpecification(Guid id) 
        : base(x => 
            x.Id == id &&
            (x.ParentId == null) &&
            (x.DeletedAt == null))
    {
        AddInclude(x => x.Include(s => s.SubCategories.Where(x => x.DeletedAt == null)).ThenInclude(s => s.SubCategories.Where(x => x.DeletedAt == null)));
    }
}