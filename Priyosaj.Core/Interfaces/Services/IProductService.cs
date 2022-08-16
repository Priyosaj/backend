using Microsoft.AspNetCore.Http;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Params;

namespace Priyosaj.Core.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponseDto>> GetAllProductsAsync(ProductSpecParams productParams);
    Task<int>CountProductsAsync(ProductSpecParams productParams);
    Task<ProductResponseDto> GetProductByIdAsync(Guid id);
    Task<ProductResponseDto> CreateProductAsync(ProductCreateReqDto product);
    Task<ProductResponseDto> UpdateProductAsync(ProductUpdateReqDto productUpdateReq, string rootPath);
    Task DeleteProductAsync(Guid id);
    Task<ProductResponseDto> UploadImages(Guid productId, string webRootPath, IFormFileCollection images);
}
