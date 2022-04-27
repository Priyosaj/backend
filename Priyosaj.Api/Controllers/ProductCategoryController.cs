using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Api.DTOs;
using Priyosaj.Contacts.Entities.Product;
using Priyosaj.Contacts.Interfaces;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Api.Controllers
{
    public class ProductCategoryController : BaseApiController
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IMapper _mapper;
        public ProductCategoryController(IProductCategoryService productCategoryService, IMapper mapper)
        {
            _mapper = mapper;
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductCategoryResponseDto>>> GetCategories(
            [FromQuery] ProductCategorySpecParams productCategoryParams
            )
        {
            var categories = await _productCategoryService.GetAllCategoriesAsync(productCategoryParams);
            var resCategories = _mapper.Map<IReadOnlyList<ProductCategoryResponseDto>>(categories);
            return Ok(resCategories);
        }

        // [HttpPost]
        // public async Task<ActionResult<ProductCategory>> CreateCategory([FromBody] ProductCategory category)
        // {
        //     // _context.ProductCategories.Add(category);
        //     // await _context.SaveChangesAsync();
        //     // return Ok(category);
        // }

    }
}