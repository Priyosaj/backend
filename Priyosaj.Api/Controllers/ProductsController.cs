using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Specifications.ProductSpecifications;

namespace Priyosaj.Api.Controllers;

public class ProductsController : BaseApiController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<Models.ApiPaginatedResponse<ProductResponseDto>>> GetProductsAsync([FromQuery] ProductSpecParams productParams)
    {
        var totalItems = await _productService.CountProductsAsync(productParams);

        var data = await _productService.GetAllProductsAsync(productParams);

        return Ok(new Models.ApiPaginatedResponse<ProductResponseDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Returning Product: " + id);

        var product = await _productService.GetProductByIdAsync(id);

        return Ok(product);
    }
}