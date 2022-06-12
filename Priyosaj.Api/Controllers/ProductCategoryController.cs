using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Contacts.DTOs.ProductCategoryDTOs;
using Priyosaj.Contacts.Interfaces.Services;
using Priyosaj.Contacts.Specifications.ProductCategorySpecifications;

namespace Priyosaj.Api.Controllers;

public class ProductCategoryController : BaseApiController
{
    private readonly IProductCategoryService _productCategoryService;
    public ProductCategoryController(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductCategoryResponseDto>>> GetCategories([FromQuery] ProductCategorySpecParams productCategoryParams)
    {
        var categories = await _productCategoryService.GetAllCategoriesAsync(productCategoryParams);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCategoryResponseDto>> GetProductAsync(Guid id)
    {
        var category = await _productCategoryService.GetCategoryByIdAsync(id);

        return Ok(category);
    }
}

