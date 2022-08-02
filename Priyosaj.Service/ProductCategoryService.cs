using AutoMapper;
using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Interfaces.Repositories;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Specifications.ProductCategorySpecifications;
using Priyosaj.Core.Utils;

namespace Priyosaj.Service;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ProductCategoryResponseDto>> GetAllCategoriesAsync(ProductCategorySpecParams productCategorySpecParams)
    {
        var spec = new ProductCategoryDemoSpecification(productCategorySpecParams);
        var categories = await _unitOfWork.Repository<ProductCategory>().ListAllAsyncWithSpec(spec);
        var resCategories = _mapper.Map<IReadOnlyList<ProductCategoryResponseDto>>(categories);
        return resCategories;
    }

    public async Task<ProductCategoryResponseDto> GetCategoryByIdAsync(Guid id)
    {
        var spec = new ProductCategoryByIdSpecification(id);

        var category = await _unitOfWork.Repository<ProductCategory>().GetEntityWithSpec(spec);

        if (category == null) throw new NotFoundException("ProductCategory not found");

        return _mapper.Map<ProductCategoryResponseDto>(category);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        category.DeletedAt = DateTime.Now;
        _unitOfWork.Repository<ProductCategory>().Update(category);
        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            throw new Exception("Category not deleted");
        }
    }

    public async Task UpdateCategoryAsync(Guid id, ProductCategoryUpdateDto category)
    {
        var existingCategory = await _unitOfWork.Repository<ProductCategory>().GetByIdAsync(id);
        if (existingCategory == null)
        {
            throw new Exception("Category not found");
        }
        existingCategory.Title = category.Title;
        existingCategory.ParentId = category.ParentId;
        _unitOfWork.Repository<ProductCategory>().Update(existingCategory);
        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            throw new Exception("Category not updated");
        }
    }

    public async Task CreateCategoryAsync(ProductCategoryCreateDto category)
    {
        var newCategory = _mapper.Map<ProductCategory>(category);
        _unitOfWork.Repository<ProductCategory>().Add(newCategory);

        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            throw new Exception("Category creation failed");
        }
    }

}
