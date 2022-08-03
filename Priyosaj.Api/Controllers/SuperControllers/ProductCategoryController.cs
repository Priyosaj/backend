using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.ProductCategoryDTOs;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Models;

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

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateCategory([FromBody] ProductCategoryCreateDto category)
    {
        await _productCategoryService.CreateCategoryAsync(category);
        return StatusCode(201, new ApiResponse(201, "Product Category Successfully Added!"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        await _productCategoryService.DeleteCategoryAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] ProductCategoryUpdateDto category)
    {
        await _productCategoryService.UpdateCategoryAsync(id, category);
        return NoContent();
    }
}