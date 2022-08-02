using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Specifications.ProductCategorySpecifications;

namespace Priyosaj.Core.Interfaces.Services;

public interface IProductCategoryService
{
    Task<IReadOnlyList<ProductCategoryResponseDto>> GetAllCategoriesAsync(ProductCategorySpecParams productCategorySpecParams);
    Task<ProductCategoryResponseDto> GetCategoryByIdAsync(Guid id);
    Task CreateCategoryAsync(ProductCategoryCreateDto category);
    Task UpdateCategoryAsync(Guid id, ProductCategoryUpdateDto category);
    Task DeleteCategoryAsync(Guid id);
}
