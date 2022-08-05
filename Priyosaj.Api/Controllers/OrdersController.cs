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
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(CreateOrderRequestDto createOrderRequestDto)
    {
        throw new NotImplementedException();
    }
    [HttpGet]
    public async Task<ActionResult<ApiDataResponse<CreateOrderRequestDto>>> GetOrdersForUser()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        throw new NotImplementedException();
    }
}