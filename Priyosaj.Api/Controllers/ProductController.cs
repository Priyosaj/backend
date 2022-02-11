using Microsoft.AspNetCore.Mvc;
using Priyosaj.Contacts.Models;

namespace Priyosaj.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductAsync()
    {
        _logger.LogInformation("Returning Product");
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Category = "Computer",
            Description = "Neque nulla quas rerum delectus ratione. Magni ducimus voluptas quos velit.",
            Title = "5 paragraphs of Lorem Ipsum",
        };
        var products = new List<Product>
        {
            product
        };
        return await Task.FromResult(Ok(products));
    }
}