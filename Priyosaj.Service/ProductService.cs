using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Entities;
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

    public async Task<ProductResponseDto> UpdateProductAsync(ProductUpdateReqDto product, string rootPath)
    {
        _currentUserService.ValidateIfEditor();
        var spec = new ProductByIdSpecification(product.Id);

        var productToUpdate = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);

        if (product == null) throw new NotFoundException("Product not found");
        productToUpdate.Title = product.Title;
        productToUpdate.Description = product.Description;
        productToUpdate.RegularPrice = product.RegularPrice;
        productToUpdate.DiscountPrice = product.DiscountPrice;

        if (product.DisplayImage != null)
        {
            productToUpdate.DisplayImageId = product.DisplayImage.Id;
            var displayImageFile = productToUpdate.Images.FirstOrDefault(img => img.Id == product.DisplayImage.Id);
            if (displayImageFile is null)
            {
                throw new NotFoundException("Display Image not found");
            }
        }

        var imagesToDelete = productToUpdate.Images.Where(img => !product.Images.Any(imgDto => imgDto.Id == img.Id)).ToList();

        var categories = new List<ProductCategory>();
        foreach (var category in productToUpdate.ProductCategories)
        {
            if (!product.ProductCategories.Contains(category.Id))
            {
                categories.Add(category);
            }
        }
        foreach (var category in categories)
        {
            productToUpdate.ProductCategories.Remove(category);
        }
        foreach (var id in product.ProductCategories)
        {
            var category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id);
            if (category == null) throw new NotFoundException("Product Category not found");
            if (productToUpdate.ProductCategories.Any(pc => pc.Id == category.Id))
            {
                continue;
            }
            productToUpdate.ProductCategories.Add(category);
            while (category.ParentId != null)
            {
                category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync((Guid)category.ParentId);
                productToUpdate.ProductCategories.Add(category);
            }
        }

        await _fileUploadService.DeleteFiles(rootPath, imagesToDelete);

        _unitOfWork.Repository<Product>().Update(productToUpdate);

        var result = await _unitOfWork.Complete();

        return _mapper.Map<ProductResponseDto>(productToUpdate);
    }

    public async Task<ProductResponseDto> UploadImages(string productId, string webRootPath, IFormFileCollection images)
    {
        _currentUserService.ValidateIfEditor();
        var files = await _fileUploadService.UploadFiles("Product", webRootPath, images);
        try
        {
            // Console.WriteLine(webRootPath);
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(Guid.Parse(productId));
            if (product == null) throw new NotFoundException("Product not found");
            foreach (var image in files)
            {
                product.Images.Add(image);
            }
            await _unitOfWork.Complete();
            return _mapper.Map<ProductResponseDto>(product);
        }
        catch (Exception e)
        {
            foreach (var file in files)
            {
                File.Delete(Path.Combine(webRootPath, file.Url));
                _unitOfWork.Repository<FileEntity>().Delete(file);
            }
            await _unitOfWork.Complete();
            throw e;
        }
    }
}