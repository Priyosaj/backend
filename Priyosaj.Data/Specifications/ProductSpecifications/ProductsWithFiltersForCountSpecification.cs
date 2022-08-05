using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Interfaces;
using Priyosaj.Core.Params;

namespace Priyosaj.Data.Specifications.ProductSpecifications;

public class ProductsWithFiltersForCountSpecification : ABaseSpecification<Product>
{
    public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Title.ToLower().Contains(productParams.Search))
        // && (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) 
        // && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    )
    {
    }
}