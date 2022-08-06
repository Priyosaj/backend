using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Entities.IdentityEntities;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Interfaces.Repositories;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Params;
using Priyosaj.Core.Utils;
using Priyosaj.Data.Specifications.ProductSpecifications;

namespace Priyosaj.Service;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileUploadService _fileUploadService;
    private readonly ICurrentUserService _currentUserService;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadService fileUploadService, ICurrentUserService currentUserService, UserManager<AppUser> userManager)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileUploadService = fileUploadService;
    }

    public async Task<IReadOnlyList<ProductResponseDto>> GetAllProductsAsync(ProductSpecParams productParams)
    {
        var spec = new ProductFetchSpecification(productParams);

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

        if (product == null) throw new NotFoundException("Product not found");

        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> CreateProductAsync(ProductCreateReqDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);

        _currentUserService.ValidateIfEditor();

        product.CreatedById = _currentUserService.UserId;

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
        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        _currentUserService.ValidateIfEditor();
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
        if (product == null) throw new NotFoundException("Product not found");
        product.DeletedAt = DateTime.Now;
        await _unitOfWork.Complete();
    }

    public Task UpdateProductAsync(Guid id, Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductResponseDto> UploadImages(string productId, string webRootPath, IFormFileCollection images)
    {
        _currentUserService.ValidateIfEditor();
        // Console.WriteLine(webRootPath);
        var files = await _fileUploadService.UploadFiles("Product", webRootPath, images);
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(Guid.Parse(productId));
        if (product == null) throw new NotFoundException("Product not found");
        foreach (var image in files)
        {
            product.Images.Add(image);
        }
        await _unitOfWork.Complete();
        return _mapper.Map<ProductResponseDto>(product);
    }   
}