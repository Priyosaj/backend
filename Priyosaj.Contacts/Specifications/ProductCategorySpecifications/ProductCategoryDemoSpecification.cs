using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Entities.ProductEntities;

namespace Priyosaj.Contacts.Specifications.ProductCategorySpecifications;

public class ProductCategoryDemoSpecification : BaseSpecification<ProductCategory>
{
    public ProductCategoryDemoSpecification() : base(x => x.DeletedAt == null)
    {
        AddInclude(x => x.Include(s => s.SubCategories.Where(x => x.DeletedAt == null)));
    }

    public ProductCategoryDemoSpecification(ProductCategorySpecParams productCategorySpecParams)
        : base(x =>
            (x.ParentId == null) &&
            (x.DeletedAt == null) &&
            (string.IsNullOrEmpty(productCategorySpecParams.Search) || x.Title.ToLower().Contains(productCategorySpecParams.Search))
        )
    {
        AddInclude(x =>
        {
            var query = x.Include(s => s.SubCategories.Where(x => x.DeletedAt == null));
            while (--productCategorySpecParams.DepthLevel > 1)
            {
                query = query.ThenInclude(s => s.SubCategories.Where(x => x.DeletedAt == null));
            }
            return query;
        });
    }

    public ProductCategoryDemoSpecification(ProductCategorySpecParamsSuper productCategorySpecParams)
        : base(x =>
            (x.ParentId == null) &&
            (productCategorySpecParams.IncludeDeletedItems.Equals(true) 
            || x.DeletedAt == null) &&
            (string.IsNullOrEmpty(productCategorySpecParams.Search) || x.Title.ToLower().Contains(productCategorySpecParams.Search))
        )
    {
        AddInclude(x =>
        {
            var query = x.Include(s => s.SubCategories.Where(x => 
                (productCategorySpecParams.IncludeDeletedItems.Equals(true) 
                || x.DeletedAt == null)));
            while (--productCategorySpecParams.DepthLevel > 1)
            {
                query = query.ThenInclude(s => s.SubCategories.Where(x => 
                    (productCategorySpecParams.IncludeDeletedItems.Equals(true) 
                    || x.DeletedAt == null)));
            }
            return query;
        });
    }

}
