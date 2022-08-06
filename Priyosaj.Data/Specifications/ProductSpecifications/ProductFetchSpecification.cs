using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Interfaces;
using Priyosaj.Core.Params;

namespace Priyosaj.Data.Specifications.ProductSpecifications;

public class ProductFetchSpecification : ABaseSpecification<Product>
{
    public ProductFetchSpecification()
    {
        AddInclude(x => x.Include(p => p.ProductCategories));
    }

    public ProductFetchSpecification(ProductSpecParams productParams)
        : base(x => 
            (string.IsNullOrEmpty(productParams.Search) || 
            x.Title.ToLower().Contains(productParams.Search))
            && 
            (!productParams.CategoryId.HasValue || 
            x.ProductCategories.Any(x => x.Id == productParams.CategoryId)
            && 
            (((productParams.Type == TypeCandidate.Active) && x.DeletedAt == null) || 
            (productParams.Type == TypeCandidate.Trash && x.DeletedAt != null) || 
            (productParams.Type == TypeCandidate.All)))
    )
    {
        AddInclude(x => x.Include(p => p.ProductCategories));
        // AddInclude(x => x.Include(p => p.Creator));

        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            ApplySorting(productParams.Sort);
        }
        else
        {
            AddOrderByDescending(x => x.Title);
        }

        /* if (!string.IsNullOrEmpty(productParams.Sort))
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
        } */
    }
}