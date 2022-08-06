using Microsoft.AspNetCore.Http;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Params;

namespace Priyosaj.Core.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponseDto>> GetAllProductsAsync(ProductSpecParams productParams);
    Task<int>CountProductsAsync(ProductSpecParams productParams);
    Task<ProductResponseDto> GetProductByIdAsync(Guid id);
    Task<ProductResponseDto> CreateProductAsync(ProductCreateReqDto product);
    Task UpdateProductAsync(Guid id, Product product);
    Task DeleteProductAsync(Guid id);
    Task<ProductResponseDto> UploadImages(string productId, string webRootPath, IFormFileCollection images);
}
