using Priyosaj.Contacts.DTOs.ProductDTOs;
using Priyosaj.Contacts.Entities.ProductEntities;
using Priyosaj.Contacts.Specifications.ProductSpecifications;

namespace Priyosaj.Contacts.Interfaces.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductResponseDto>> GetAllProductsAsync(ProductSpecParams productParams);
    Task<int>CountProductsAsync(ProductSpecParams productParams);
    Task<ProductResponseDto> GetProductByIdAsync(Guid id);
    Task CreateProductAsync(ProductCreateDto product);
    Task UpdateProductAsync(Guid id, Product product);
    Task DeleteProductAsync(Guid id);
}
