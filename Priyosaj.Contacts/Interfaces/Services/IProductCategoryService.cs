using Priyosaj.Contacts.DTOs.ProductCategoryDTOs;
using Priyosaj.Contacts.Entities.ProductEntities;
using Priyosaj.Contacts.Specifications.ProductCategorySpecifications;

namespace Priyosaj.Contacts.Interfaces.Services;

public interface IProductCategoryService
{
    Task<IReadOnlyList<ProductCategoryResponseDto>> GetAllCategoriesAsync(ProductCategorySpecParams productCategorySpecParams);
    Task<ProductCategoryResponseDto> GetCategoryByIdAsync(Guid id);
    Task CreateCategoryAsync(ProductCategoryCreateDto category);
    Task UpdateCategoryAsync(Guid id, ProductCategoryUpdateDto category);
    Task DeleteCategoryAsync(Guid id);
}
