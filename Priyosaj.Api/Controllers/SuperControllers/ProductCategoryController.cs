using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Api.DTOs.ProductCategoryDTOs;
using Priyosaj.Contacts.Entities.IdentityEntities;
using Priyosaj.Contacts.Entities.ProductEntities;
using Priyosaj.Contacts.Interfaces.Services;

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
    public async Task<ActionResult> CreateCategory([FromBody] ProductCategoryCreateDto category)
    {
        var productCategory = _mapper.Map<ProductCategory>(category);
        await _productCategoryService.CreateCategoryAsync(productCategory);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        await _productCategoryService.DeleteCategoryAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory([FromRoute]Guid id, [FromBody] ProductCategoryCreateDto category)
    {
        var productCategory = _mapper.Map<ProductCategory>(category);
        await _productCategoryService.UpdateCategoryAsync(id, productCategory);
        return NoContent();
    }
}