using Priyosaj.Contacts.Entities.Product;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Contacts.Interfaces.Services;

public interface IProductCategoryService
{
    Task<IReadOnlyList<ProductCategory>> GetAllCategoriesAsync(ProductCategorySpecParams productCategorySpecParams);
    Task<ProductCategory> GetCategoryByIdAsync(Guid id);
    Task CreateCategoryAsync(ProductCategory category);
    Task UpdateCategoryAsync(ProductCategory category);
    Task DeleteCategoryAsync(Guid id);
}
