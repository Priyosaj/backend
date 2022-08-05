using Priyosaj.Core.DTOs.OrderDTOs;
using Priyosaj.Core.Entities.OrderEntities;

namespace Priyosaj.Core.Interfaces.Services;

public interface IOrderService
{
    Task<IReadOnlyList<OrderToReturnDto>> GetAllOrders();
    Task<OrderToReturnDto> GetOrderByIdAsync(Guid orderId);
    Task<OrderToReturnDto> CreateOrderAsync(CreateOrderRequestDto orderReq);
    Task<IReadOnlyList<OrderToReturnDto>> GetOrdersForUserAsync();
    Task<OrderToReturnDto> GetCustomerOrderByIdAsync(Guid orderId);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}