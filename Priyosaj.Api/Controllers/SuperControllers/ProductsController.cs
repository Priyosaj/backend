using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.ProductDTOs;
using Priyosaj.Core.Interfaces.Services;

namespace Priyosaj.Api.Controllers.SuperControllers;

public class ProductsController : BaseEditorSuperController
{
    private ILogger<ProductsController> _logger;
    private IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProductAsync(ProductCreateReqDto product)
    {
        await _productService.CreateProductAsync(product);
        return NoContent();
    }
}