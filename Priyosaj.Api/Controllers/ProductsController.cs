using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Api.DTOs;
using Priyosaj.Api.Errors;
using Priyosaj.Api.Helpers;
using Priyosaj.Business.Data;
using Priyosaj.Contacts.Entities.Product;
using Priyosaj.Contacts.Interfaces.Repositories;
using Priyosaj.Contacts.Specifications.ProductSpecifications;

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
    public async Task<ActionResult<PaginatedResponse<ProductResponseDto>>> GetProductsAsync(
        [FromQuery] ProductSpecParams productParams)
    {
        var spec = new ProductDemoSpecification(productParams);
        var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

        var totalItems = await _productRepo.CountAsync(countSpec);

        var products = await _productRepo.ListAllAsyncWithSpec(spec);

        var data = _mapper.Map<IReadOnlyList<ProductResponseDto>>(products);

        return Ok(new PaginatedResponse<ProductResponseDto>(productParams.PageIndex,
            productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProductAsync(Guid id)
    {
        _logger.LogInformation("Returning Product: " + id);
        var spec = new ProductByIdSpecification(id);

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