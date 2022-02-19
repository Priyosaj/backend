using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Business.Data;
using Priyosaj.Contacts.Models;

namespace Priyosaj.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly StoreContext _context;

    public ProductsController(ILogger<ProductsController> logger, StoreContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProductsAsync()
    {
        _logger.LogInformation("Returning Products");

        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Returning Product: " + id);

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateProductAsync(Product product)
    {
        _logger.LogInformation("Creating Product: ");

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return Ok(product);
    }
}