using AutoMapper;
using Priyosaj.Contacts.DTOs.ProductDTOs;
using Priyosaj.Contacts.Entities.ProductEntities;
using Priyosaj.Contacts.Interfaces.Repositories;
using Priyosaj.Contacts.Interfaces.Services;
using Priyosaj.Contacts.Specifications.ProductSpecifications;
using Priyosaj.Contacts.Utils;

namespace Priyosaj.Business.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductResponseDto>> GetAllProductsAsync(ProductSpecParams productParams)
    {
        var spec = new ProductDemoSpecification(productParams);

        var products = await _unitOfWork.Repository<Product>().ListAllAsyncWithSpec(spec);

        var data = _mapper.Map<IReadOnlyList<ProductResponseDto>>(products);

        return data;
    }

    public async Task<int> CountProductsAsync(ProductSpecParams productParams)
    {
        var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

        var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);

        return totalItems;
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(Guid id)
    {
        var spec = new ProductByIdSpecification(id);

        var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);

        if(product == null) throw new NotFoundException("Product not found");

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task CreateProductAsync(ProductCreateDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);

        product.ProductCategories = new List<ProductCategory>();
        try
        {
            foreach (var id in productDto.ProductCategoriesId)
            {
                var category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id);
                product.ProductCategories.Add(category);

                while (category.ParentId != null)
                {
                    category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync((Guid)category.ParentId);
                    product.ProductCategories.Add(category);
                }
            }
            _unitOfWork.Repository<Product>().Add(product);
        }
        catch (System.NullReferenceException e)
        {
            throw new Exception("Product Category not found");
        }

        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            throw new Exception("Error while creating product");
        }
    }

    public Task DeleteProductAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(Guid id, Product product)
    {
        throw new NotImplementedException();
    }
}