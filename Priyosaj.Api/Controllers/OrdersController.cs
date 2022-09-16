using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.OrderDTOs;
using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Models;

namespace Priyosaj.Api.Controllers;
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public OrdersController(IOrderService orderService, IMapper mapper, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _currentUserService = currentUserService;
        _orderService = orderService;
    }
    
    [HttpPost] public async Task<ActionResult<ApiDataResponse<OrderToReturnDto>>> CreateOrder(CreateOrderRequestDto createOrderRequestDto)
    {
        var order = await _orderService.CreateOrderAsync(createOrderRequestDto);

        return StatusCode(201, new ApiDataResponse<OrderToReturnDto>(order, 201, "Order Creation Successful!"));
    }

    [HttpGet] public async Task<ActionResult<ApiDataResponse<IReadOnlyList<OrderToReturnDto>>>> GetOrdersForUser()
    {
        var orders = await _orderService.GetOrdersForUserAsync();

        return StatusCode(200, new ApiDataResponse<IReadOnlyList<OrderToReturnDto>>(orders, 200, "Orders Successfully Fetched!"));
    }
    
    [HttpGet("deliveryMethods")] public async Task<ActionResult<ApiDataResponse<IReadOnlyList<DeliveryMethod>>>> GetDeliveryMethods()
    {
        var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
        
        return StatusCode(200, new ApiDataResponse<IReadOnlyList<DeliveryMethod>>(deliveryMethods, 200, "DeliveryMethods Successfully Fetched!"));
    }
}