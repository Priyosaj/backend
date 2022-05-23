using Priyosaj.Contacts.Entities.Product;
using Priyosaj.Contacts.Interfaces.Repositories;
using Priyosaj.Contacts.Interfaces.Services;
using Priyosaj.Contacts.Specifications.ProductCategorySpecifications;

namespace Priyosaj.Business.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ProductCategoryService(IUnitOfWork unitOfWork, IGenericRepository<ProductCategory> productCategoryRepository)
    {
        _unitOfWork = unitOfWork;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<IReadOnlyList<ProductCategory>> GetAllCategoriesAsync(ProductCategorySpecParams productCategorySpecParams)
    {
        var spec = new ProductCategoryDemoSpecification(productCategorySpecParams);
        var categories = await _productCategoryRepository.ListAllAsyncWithSpec(spec);
        categories = categories.Where(c => c.ParentId == null).ToList();
        return categories;
    }

    public Task<ProductCategory> GetCategoryByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _productCategoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        category.DeletedAt = DateTime.Now;
        _unitOfWork.Repository<ProductCategory>().Update(category);
        var result = await _unitOfWork.Complete();
        if (result<=0)
        {
            throw new Exception("Category not deleted");
        }
    }

    public async Task UpdateCategoryAsync(Guid id, ProductCategory category)
    {
        var existingCategory = await _productCategoryRepository.GetByIdAsync(id);
        if(existingCategory==null)
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

    public async Task CreateCategoryAsync(ProductCategory category)
    {
        _unitOfWork.Repository<ProductCategory>().Add(category);
        
        var result = await _unitOfWork.Complete(); 
        if (result <= 0)
        {
            throw new Exception("Category creation failed");
        }
    }
}
