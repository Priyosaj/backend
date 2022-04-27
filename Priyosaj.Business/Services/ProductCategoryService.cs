using Microsoft.Extensions.Logging;
using Priyosaj.Contacts.Entities.Product;
using Priyosaj.Contacts.Interfaces;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Business.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductCategoryService(IGenericRepository<ProductCategory> productCategoryRepository, IUnitOfWork unitOfWork)
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

        public Task DeleteCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(ProductCategory category)
        {
            throw new NotImplementedException();
        }

        public Task CreateCategoryAsync(ProductCategory category)
        {
            throw new NotImplementedException();
        }
    }
}