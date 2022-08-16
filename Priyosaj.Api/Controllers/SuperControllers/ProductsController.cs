using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Models;
using Priyosaj.Core.Params;

namespace Priyosaj.Api.Controllers.SuperControllers;

public class ProductsController : BaseEditorSuperController
{
    private ILogger<ProductsController> _logger;
    private IProductService _productService;
    private readonly IWebHostEnvironment _env;

    public ProductsController(IWebHostEnvironment env, ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
        _env = env;
    }

    [HttpGet] public async Task<ActionResult<ApiPaginatedResponse<ProductResponseDto>>> GetProductsAsync([FromQuery] ProductSpecParams productParams)
    {
        var totalItems = await _productService.CountProductsAsync(productParams);

        var data = await _productService.GetAllProductsAsync(productParams);

        return StatusCode(200, new ApiPaginatedResponse<ProductResponseDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")] public async Task<ActionResult<ApiDataResponse<ProductResponseDto>>> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Returning Product: " + id);

        var product = await _productService.GetProductByIdAsync(id);

        return StatusCode(200, new ApiDataResponse<ProductResponseDto>(product, 200, "Successful!"));
    }

    [HttpPost] public async Task<ActionResult<ApiDataResponse<ProductResponseDto>>> CreateProductAsync(ProductCreateReqDto product)
    {
        var createdProduct = await _productService.CreateProductAsync(product);
        return StatusCode(201, new ApiDataResponse<ProductResponseDto>(createdProduct, 201, "Product Creation Successful!"));
    }

    [HttpPatch("{productId}")] public async Task<ActionResult<ApiDataResponse<ProductResponseDto>>> UploadImages([FromRoute]Guid productId)
    {
        var product = await _productService.UploadImages(productId, _env.WebRootPath, Request.Form.Files);
        return StatusCode(201, new ApiDataResponse<ProductResponseDto>(product, 201, "Image Upload Successful!"));
    }

    [HttpDelete("{productId}")] public async Task<ActionResult<ApiResponse>> DeleteProductAsync([FromRoute]Guid productId)
    {
        await _productService.DeleteProductAsync(productId);
        return StatusCode(201, new ApiResponse(204, "Product Deletion Successful!"));
    }

    /*Product Update*/
    [HttpPatch] public async Task<ActionResult<ApiDataResponse<ProductResponseDto>>> UpdateProductAsync([FromBody]ProductUpdateReqDto product)
    {
        var updatedProduct = await _productService.UpdateProductAsync(product, _env.WebRootPath);
        return StatusCode(201, new ApiDataResponse<ProductResponseDto>(updatedProduct, 201, "Product Update Successful!"));
    }
    
}