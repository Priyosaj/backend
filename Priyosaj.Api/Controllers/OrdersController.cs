// using System.Security.Claims;
// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using Priyosaj.Contacts.DTOs;
// using Priyosaj.Api.Errors;
// using Priyosaj.Api.Extensions;
// using Priyosaj.Contacts.Entities.Identity;
// using Priyosaj.Contacts.Entities.Order;
//
// namespace Priyosaj.Api.Controllers;
//
// public class OrdersController : BaseApiController
// {
//     private readonly IOrderService _orderService;
//     private readonly IMapper _mapper;
//
//     public OrdersController(IOrderService orderService, IMapper mapper)
//     {
//         _mapper = mapper;
//         _orderService = orderService;
//     }
//
//     [HttpPost]
//     public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
//     {
//         var email = User.GetUserEmail();
//
//         var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
//
//         var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId,
//             address);
//
//         if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
//
//         return Ok(order);
//     }
//
//     [HttpGet]
//     public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
//     {
//         var email = User.GetUserEmail();
//
//         var orders = await _orderService.GetOrdersForUserAsync(email);
//
//         return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
//     }
//
//     [HttpGet("{id}")]
//     public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
//     {
//         var email = User.GetUserEmail();
//
//         var order = await _orderService.GetOrderByIdAsync(id, email);
//
//         if (order == null) return NotFound(new ApiResponse(404));
//
//         return _mapper.Map<OrderToReturnDto>(order);
//     }
//
//     [HttpGet("deliveryMethods")]
//     public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
//     {
//         return Ok(await _orderService.GetDeliveryMethodsAsync());
//     }
// }