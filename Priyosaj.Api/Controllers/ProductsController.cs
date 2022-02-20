using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priyosaj.Api.DTOs;
using Priyosaj.Business.Data;
using Priyosaj.Contacts.Interfaces;
using Priyosaj.Contacts.Models;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IMapper _mapper;

    public ProductsController(ILogger<ProductsController> logger, IGenericRepository<Product> productRepo,
        IMapper mapper)
    {
        _logger = logger;
        _productRepo = productRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProductsAsync()
    {
        _logger.LogInformation("Returning Products");
        var spec = new ProductDemoSpecification();

        var products = await _productRepo.ListAllAsyncWithSpec(spec);
        
        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductResponseDto>>(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Returning Product: " + id);
        var spec = new ProductDemoSpecification(id);

        var product = await _productRepo.GetEntityWithSpec(spec);
        if (product == null) return NotFound();

        return Ok(_mapper.Map<Product, ProductResponseDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult> CreateProductAsync(Product product)
    {
        _logger.LogInformation("Creating Product: ");

        // await _context.Products.AddAsync(product);
        // await _context.SaveChangesAsync();
        return Ok("HIT");
    }
}