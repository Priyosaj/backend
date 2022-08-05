using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Models;

namespace Priyosaj.Api.Controllers.SuperControllers;

public class ProductsController : BaseEditorSuperController
{
    private ILogger<ProductsController> _logger;
    private IProductService _productService;
    private readonly IWebHostEnvironment _env;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService, IWebHostEnvironment env)
    {
        _logger = logger;
        _productService = productService;
        _env = env;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProductAsync(ProductCreateReqDto product)
    {
        await _productService.CreateProductAsync(product);
        return NoContent();
    }

    [HttpPatch("{productId}")]
    public async Task<ActionResult> UploadImages([FromQuery] string productId, [FromBody] IFormFileCollection images)
    {
        await _productService.UploadImages(productId, images, _env.WebRootPath);
        return StatusCode(204, new ApiResponse(204, "Image Upload Successful!"));
    }


    /*Product Update, Delete*/
    
}