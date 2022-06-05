using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Contacts.DTOs.ProductCategoryDTOs;
using Priyosaj.Contacts.Interfaces.Services;
using Priyosaj.Contacts.Specifications.ProductCategorySpecifications;

namespace Priyosaj.Api.Controllers;

public class ProductCategoryController : BaseApiController
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly IMapper _mapper;
    public ProductCategoryController(IProductCategoryService productCategoryService, IMapper mapper)
    {
        _productCategoryService = productCategoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductCategoryResponseDto>>> GetCategories(
        [FromQuery] ProductCategorySpecParams productCategoryParams
        )
    {
        var categories = await _productCategoryService.GetAllCategoriesAsync(productCategoryParams);
        var resCategories = _mapper.Map<IReadOnlyList<ProductCategoryResponseDto>>(categories);
        return Ok(resCategories);
    }
}
