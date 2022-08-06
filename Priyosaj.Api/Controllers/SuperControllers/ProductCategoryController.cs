using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Models;
using Priyosaj.Core.Params;

namespace Priyosaj.Api.Controllers.SuperControllers;

public class ProductCategoryController : BaseEditorSuperController
{
    private readonly IProductCategoryService _productCategoryService;
    private readonly IMapper _mapper;

    public ProductCategoryController(IProductCategoryService productCategoryService, IMapper mapper)
    {
        _mapper = mapper;
        _productCategoryService = productCategoryService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ApiDataResponse<IReadOnlyList<ProductCategoryResponseDto>>>> GetAllCategories([FromQuery]ProductCategorySpecParams productCategorySpecParams)
    {
        var categories = await _productCategoryService.GetAllCategoriesAsync(productCategorySpecParams);
        return StatusCode(200, new ApiDataResponse<IReadOnlyList<ProductCategoryResponseDto>>(categories,200, "Product Category Successfully Fetched!"));
    }

    [HttpPost]
    public async Task<ActionResult<ApiDataResponse<ProductCategoryResponseDto>>> CreateCategory([FromBody] ProductCategoryCreateDto category)
    {
        var createdCategory = await _productCategoryService.CreateCategoryAsync(category);
        return StatusCode(201, new ApiDataResponse<ProductCategoryResponseDto>(createdCategory,201, "Product Category Successfully Added!"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteCategory(Guid id)
    {
        await _productCategoryService.DeleteCategoryAsync(id);
        return StatusCode(204, new ApiResponse(204, "Product Category Successfully Deleted!"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiDataResponse<ProductCategoryResponseDto>>> UpdateCategory([FromRoute] Guid id, [FromBody] ProductCategoryUpdateDto category)
    {
        var updateCategory = await _productCategoryService.UpdateCategoryAsync(id, category);
        return StatusCode(200, new ApiDataResponse<ProductCategoryResponseDto>(updateCategory,200, "Product Category Successfully Updated!"));
    }
}