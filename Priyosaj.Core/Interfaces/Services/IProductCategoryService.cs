using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Params;

namespace Priyosaj.Core.Interfaces.Services;

public interface IProductCategoryService
{
    Task<IReadOnlyList<ProductCategoryResponseDto>> GetAllCategoriesAsync(ProductCategorySpecParams productCategorySpecParams);
    Task<ProductCategoryResponseDto> GetCategoryByIdAsync(Guid id);
    Task<ProductCategoryResponseDto> CreateCategoryAsync(ProductCategoryCreateDto category);
    Task<ProductCategoryResponseDto> UpdateCategoryAsync(Guid id, ProductCategoryUpdateDto category);
    Task DeleteCategoryAsync(Guid id);
}
