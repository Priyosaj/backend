using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.Entities.BasketEntities;
using Priyosaj.Core.Interfaces.Repositories;
using Priyosaj.Core.Models;

namespace Priyosaj.Api.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<ApiDataResponse<CustomerBasket>>> GetBasketById(Guid userId)
    {
        var basket = (await _basketRepository.GetUserBasketAsync(userId)) ?? new CustomerBasket(userId);

        return Ok(new ApiDataResponse<CustomerBasket>(basket, 200));
    }

    [HttpPut("{userId}")]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket([FromQuery] Guid userId,
        [FromBody] CustomerBasket basket)
    {
        var updatedBasket = (await _basketRepository.UpdateUserBasketAsync(basket)) ?? new CustomerBasket(userId);

        return Ok(new ApiDataResponse<CustomerBasket>(updatedBasket, 200));
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<ApiResponse>> DeleteBasketAsync(Guid userId)
    {
        await _basketRepository.DeleteUserBasketAsync(userId);
        return StatusCode(204, new ApiResponse(204, "Basket Deleted"));
    }
}