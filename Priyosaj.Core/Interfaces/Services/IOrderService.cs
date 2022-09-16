using Priyosaj.Core.DTOs.OrderDTOs;
using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.Params;

namespace Priyosaj.Core.Interfaces.Services;

public interface IOrderService
{
    Task<IReadOnlyList<OrderToReturnDto>> GetAllOrdersAsync(OrderFetchSpecificationParams orderParams);
    Task<int> CountOrdersAsync(OrderFetchSpecificationParams orderParams);
    Task<OrderToReturnDto> GetOrderByIdAsync(Guid orderId);
    Task<OrderToReturnDto> CreateOrderAsync(CreateOrderRequestDto orderReq);
    Task<IReadOnlyList<OrderToReturnDto>> GetOrdersForUserAsync();
    Task<OrderToReturnDto> GetCustomerOrderByIdAsync(Guid orderId);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}