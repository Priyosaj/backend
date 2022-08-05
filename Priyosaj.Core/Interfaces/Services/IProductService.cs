using Microsoft.AspNetCore.Http;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Specifications.ProductSpecifications;

namespace Priyosaj.Core.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponseDto>> GetAllProductsAsync(ProductSpecParams productParams);
    Task<int>CountProductsAsync(ProductSpecParams productParams);
    Task<ProductResponseDto> GetProductByIdAsync(Guid id);
    Task CreateProductAsync(ProductCreateReqDto product);
    Task UpdateProductAsync(Guid id, Product product);
    Task DeleteProductAsync(Guid id);
    Task UploadImages(string productId, IFormFileCollection images, string webRootPath);
}
