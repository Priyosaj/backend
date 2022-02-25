using Priyosaj.Contacts.Models;

namespace Priyosaj.Contacts.Specifications;

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