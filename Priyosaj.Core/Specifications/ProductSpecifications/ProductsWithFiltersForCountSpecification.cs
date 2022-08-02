using Priyosaj.Core.Entities.ProductEntities;

namespace Priyosaj.Core.Specifications.ProductSpecifications;

public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Title.ToLower().Contains(productParams.Search))
        // && (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) 
        // && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
    )
    {
    }
}