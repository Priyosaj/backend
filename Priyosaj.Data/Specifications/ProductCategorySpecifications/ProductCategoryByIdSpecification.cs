using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Interfaces;

namespace Priyosaj.Data.Specifications.ProductCategorySpecifications;

public class ProductCategoryByIdSpecification: ABaseSpecification<ProductCategory>
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