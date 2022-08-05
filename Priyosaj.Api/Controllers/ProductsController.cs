using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Models;
using Priyosaj.Core.Params;

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
    public async Task<ActionResult<ApiPaginatedResponse<ProductResponseDto>>> GetProductsAsync([FromQuery] ProductSpecParams productParams)
    {
        var totalItems = await _productService.CountProductsAsync(productParams);

        var data = await _productService.GetAllProductsAsync(productParams);

        return Ok(new ApiPaginatedResponse<ProductResponseDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Returning Product: " + id);

        var product = await _productService.GetProductByIdAsync(id);

        return Ok(product);
    }
}