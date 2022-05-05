using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Entities.Product;

namespace Priyosaj.Contacts.Specifications;

public class ProductCategoryDemoSpecification : BaseSpecification<ProductCategory>
{
    public ProductCategoryDemoSpecification()
    {
        AddInclude(x => x.Include(s => s.SubCategories));
    }

    public ProductCategoryDemoSpecification(ProductCategorySpecParams productCategorySpecParams)
        : base(x =>
            (x.ParentId == null) &&
            (string.IsNullOrEmpty(productCategorySpecParams.Search) || x.Title.ToLower().Contains(productCategorySpecParams.Search))
        )
    {
        AddInclude(x =>
        {
            var query = x.Include(s => s.SubCategories);
            while (--productCategorySpecParams.DepthLevel > 1) query = query.ThenInclude(s => s.SubCategories);
            return query;
        });
    }
}
