using Microsoft.AspNetCore.Mvc;
using Priyosaj.Api.Errors;
using Priyosaj.Business.Helpers;
using Priyosaj.Contacts.DTOs.ProductDTOs;
using Priyosaj.Contacts.Entities.ProductEntities;
using Priyosaj.Contacts.Interfaces.Services;
using Priyosaj.Contacts.Specifications.ProductSpecifications;

namespace Priyosaj.Api.Controllers;

public class ProductsController : BaseApiController
{
    private ILogger<ProductsController> _logger;
    private IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<ProductResponseDto>>> GetProductsAsync([FromQuery] ProductSpecParams productParams)
    {
        var totalItems = await _productService.CountProductsAsync(productParams);

        var data = await _productService.GetAllProductsAsync(productParams);

        return Ok(new PaginatedResponse<ProductResponseDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Returning Product: " + id);

        var product = await _productService.GetProductByIdAsync(id);

        return Ok(product);
    }
}