using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Interfaces;
using Priyosaj.Core.Params;

namespace Priyosaj.Data.Specifications.ProductSpecifications;

public class ProductsWithFiltersForCountSpecification : ABaseSpecification<Product>
{
    public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)
        : base(
            x =>
                (
                    string.IsNullOrEmpty(productParams.Search)
                    || x.Title.ToLower().Contains(productParams.Search)
                )
                && (
                    !productParams.CategoryId.HasValue
                    || x.ProductCategories.Any(x => x.Id == productParams.CategoryId)
                )
                && (
                    ((productParams.Type == TypeCandidate.Active) && x.DeletedAt == null)
                    || (productParams.Type == TypeCandidate.Trash && x.DeletedAt != null)
                    || (
                        productParams.Type == TypeCandidate.All && x.DeletedAt != null
                        || x.DeletedAt == null
                    )
                )
        ) { }
}
