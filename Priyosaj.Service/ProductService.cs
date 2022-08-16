using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Http;
// using Newtonsoft.Json;

using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Entities;
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

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IFileUploadService fileUploadService,
        ICurrentUserService currentUserService)
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

        await AddProductCategories(productDto.ProductCategoriesId, product);
        _unitOfWork.Repository<Product>().Add(product);

        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            throw new Exception("Error while creating product");
        }
        // Console.WriteLine("-------------------");
        // Console.WriteLine("-------------------");
        // Console.WriteLine("-------------------");
        // Console.WriteLine("-------------------");
        // Console.WriteLine("-------------------");
        // Console.WriteLine("-------------------");
        // // Console.WriteLine(product.Specification);
        // // var tmp = JsonSerializer.Deserialize<ICollection<Object>>(product.Specification);
        // foreach (var obj in tmp)
        // {
        //     Console.WriteLine(obj);
        // }
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
        await AddProductCategories(productUpdateReq.NewProductCategories, updatedProduct);

        _unitOfWork.Repository<Product>().Update(updatedProduct);

        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            throw new BadRequestException("Product Update Failed!");
        }

        return _mapper.Map<ProductResponseDto>(updatedProduct);
    }
    
    private async Task AddProductCategories(ICollection<Guid>? productCategoriesToAdd, Product updatedProduct)
    {
        if (productCategoriesToAdd == null) return;
        var newCategoriesToAdd = new List<ProductCategory>();
        foreach (var id in productCategoriesToAdd)
        {
            var category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id);
            if (category == null) throw new NotFoundException("Product Category not found");
            if (category.SubCategories != null && category.SubCategories.Count > 0)
            {
                throw new BadRequestException("Not a leaf node category");
            }

            newCategoriesToAdd.Add(category);
            while (category.ParentId != null)
            {
                category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(category.ParentId.Value);
                if (category == null)
                {
                    throw new NotFoundException("Parent Category Not Found");
                }

                newCategoriesToAdd.Add(category);
            }
        }

        updatedProduct.ProductCategories = newCategoriesToAdd;
    }

    private async Task HandleProductImagesUpdate(ProductUpdateReqDto productUpdateReq, Product updatedProduct,
        string rootPath)
    {
        if (productUpdateReq.ImagesToDelete == null)
        {
            return;
        }

        if (updatedProduct.DisplayImageId != null &&
            productUpdateReq.ImagesToDelete.Contains(updatedProduct.DisplayImageId.Value))
        {
            throw new BadRequestException(
                "You cannot delete the display image of a product. Please select another image as your display image!");
        }

        var imagesToDelete = updatedProduct.Images.Where(img => productUpdateReq.ImagesToDelete.Contains(img.Id))
            .ToList();
        await _fileUploadService.DeleteFiles(rootPath, imagesToDelete);
        updatedProduct.Images =
            updatedProduct.Images.Where(i => !productUpdateReq.ImagesToDelete.Contains(i.Id)).ToList();
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