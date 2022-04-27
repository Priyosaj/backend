using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Entities.Product;

namespace Priyosaj.Contacts.Specifications;

public class ProductDemoSpecification : BaseSpecification<Product>
{
    public ProductDemoSpecification()
    {
        AddInclude(x => x.Include(p => p.ProductCategories));
    }

    public ProductDemoSpecification(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Title.ToLower().Contains(productParams.Search))
            && (!productParams.CategoryId.HasValue || x.ProductCategories.Any(x => x.Id == productParams.CategoryId))
        // && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    )
    {
        AddInclude(x => x.Include(p => p.ProductCategories));

        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.RegularPrice);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.RegularPrice);
                    break;
                default:
                    AddOrderBy(p => p.Title);
                    break;
            }
        }
        else
        {
            AddOrderByDescending(x => x.Title);
        }
    }
}