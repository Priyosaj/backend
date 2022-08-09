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

    public async Task<ProductResponseDto> UpdateProductAsync(ProductUpdateReqDto productUpdateReq, string rootPath)
    {
        _currentUserService.ValidateIfEditor();
        
        var spec = new ProductByIdSpecification(productUpdateReq.Id);
        var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);
        if (product == null) throw new NotFoundException("Product not found");

        var updatedProduct = _mapper.Map<Product>(productUpdateReq);

        await HandleProductImagesUpdate(productUpdateReq, updatedProduct, rootPath);
        HandleDisplayImageUpdate(productUpdateReq, updatedProduct);
        await HandleProductCategoryUpdate(productUpdateReq, updatedProduct);
        
        _unitOfWork.Repository<Product>().Update(updatedProduct);

        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            throw new BadRequestException("Product Update Failed!");
        }

        return _mapper.Map<ProductResponseDto>(updatedProduct);
    }

    private async Task HandleProductCategoryUpdate(ProductUpdateReqDto productUpdateReq, Product updatedProduct)
    {
        var categories = new List<ProductCategory>();
        foreach (var category in updatedProduct.ProductCategories)
        {
            if (!productUpdateReq.ProductCategories.Contains(category.Id))
            {
                categories.Add(category);
            }
        }

        foreach (var category in categories)
        {
            updatedProduct.ProductCategories.Remove(category);
        }

        foreach (var id in productUpdateReq.ProductCategories)
        {
            var category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id);
            if (category == null) throw new NotFoundException("Product Category not found");
            if (updatedProduct.ProductCategories.Any(pc => pc.Id == category.Id))
            {
                continue;
            }

            updatedProduct.ProductCategories.Add(category);
            while (category.ParentId != null)
            {
                category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync((Guid)category.ParentId);
                updatedProduct.ProductCategories.Add(category);
            }
        }
    }

    private async Task HandleProductImagesUpdate(ProductUpdateReqDto productUpdateReq, Product updatedProduct, string rootPath)
    {
        if (productUpdateReq.ImagesToDelete == null)
        {
            return;
        }

        if (updatedProduct.DisplayImageId != null && productUpdateReq.ImagesToDelete.Contains(updatedProduct.DisplayImageId.Value))
        {
            throw new BadRequestException("You cannot delete the display image of a product. Please select another image as your display image!");
        }
        
        var imagesToDelete = updatedProduct.Images.Where(img => productUpdateReq.ImagesToDelete.Contains(img.Id)).ToList();
        await _fileUploadService.DeleteFiles(rootPath, imagesToDelete);
        updatedProduct.Images = updatedProduct.Images.Where(i => !productUpdateReq.ImagesToDelete.Contains(i.Id)).ToList();
    }

    private void HandleDisplayImageUpdate(ProductUpdateReqDto productUpdateReq, Product product)
    {
        if (productUpdateReq.DisplayImageToUpdate == null) return;
        
        var displayImageFile = product.Images.FirstOrDefault(img => img.Id == productUpdateReq.DisplayImageToUpdate.Id);
        if (displayImageFile is null)
        {
            throw new NotFoundException("Display Image not found");
        }
        product.DisplayImageId = productUpdateReq.DisplayImageToUpdate.Id;
    }

    public async Task<ProductResponseDto> UploadImages(Guid productId, string webRootPath, IFormFileCollection images)
    {
        _currentUserService.ValidateIfEditor();
        var files = await _fileUploadService.UploadFiles("Product", webRootPath, images);
        try
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
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