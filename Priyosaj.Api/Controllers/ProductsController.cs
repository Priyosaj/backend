using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Api.DTOs;
using Priyosaj.Api.Errors;
using Priyosaj.Business.Data;
using Priyosaj.Contacts.Interfaces;
using Priyosaj.Contacts.Models;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Api.Controllers;

public class ProductsController : BaseApiController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IMapper _mapper;
    private readonly StoreContext _context;

    public ProductsController(ILogger<ProductsController> logger, IGenericRepository<Product> productRepo,
        IMapper mapper, StoreContext context)
    {
        _logger = logger;
        _productRepo = productRepo;
        _mapper = mapper;
        _context = context;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Returning Product: " + id);
        var spec = new ProductDemoSpecification(id);

        var product = await _productRepo.GetEntityWithSpec(spec);
        
        if (product == null) return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<Product, ProductResponseDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult> CreateProductAsync(ProductCreateDto product)
    {
        _logger.LogInformation("Creating Product: ");

        await _context.Products.AddAsync(_mapper.Map<ProductCreateDto, Product>(product));
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}