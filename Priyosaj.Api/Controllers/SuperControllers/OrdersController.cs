using Microsoft.AspNetCore.Mvc;
using Priyosaj.Core.DTOs.OrderDTOs;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Models;
using Priyosaj.Core.Params;

namespace Priyosaj.Api.Controllers.SuperControllers;

public class OrdersController : BaseEditorSuperController
{
    private ILogger<OrdersController> _logger;
    private IOrderService _orderService;
    private readonly IWebHostEnvironment _env;

    public OrdersController(
        ILogger<OrdersController> logger,
        IOrderService orderService,
        IWebHostEnvironment env
    )
    {
        _logger = logger;
        _orderService = orderService;
        _env = env;
    }

    [HttpGet]
    public async Task<ActionResult<ApiPaginatedResponse<OrderToReturnDto>>> GetAllOrders(
        [FromQuery] OrderFetchSpecificationParams orderParams
    )
    {
        var totalItems = await _orderService.CountOrdersAsync(orderParams);

        var data = await _orderService.GetAllOrdersAsync(orderParams);

        return StatusCode(
            200,
            new ApiPaginatedResponse<OrderToReturnDto>(
                orderParams.PageIndex,
                orderParams.PageSize,
                totalItems,
                data
            )
        );
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<ApiDataResponse<OrderToReturnDto>>> GetOrder(Guid orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        return StatusCode(
            200,
            new ApiDataResponse<OrderToReturnDto>(order, 200, "Order Successfully Fetched!")
        );
    }
}
