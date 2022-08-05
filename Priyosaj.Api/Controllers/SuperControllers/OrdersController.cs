using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.OrderDTOs;
using Priyosaj.Core.Interfaces.Services;

namespace Priyosaj.Api.Controllers.SuperControllers;

public class OrdersController : BaseEditorSuperController
{
    private ILogger<OrdersController> _logger;
    private IOrderService _orderService;
    private readonly IWebHostEnvironment _env;

    public OrdersController(ILogger<OrdersController> logger, IOrderService orderService, IWebHostEnvironment env)
    {
        _logger = logger;
        _orderService = orderService;
        _env = env;
    }

    [HttpGet("{customerId}")]
    public async Task<ActionResult<OrderToReturnDto>> GetCustomerOrders(Guid customerId)
    {
        throw new NotImplementedException();
        // var email = User.GetUserEmail();
        // var order = await _orderService.GetOrderByIdAsync(id, email);
        // if (order == null) return NotFound(new ApiResponse(404));
        // return _mapper.Map<OrderToReturnDto>(order);
    }
}