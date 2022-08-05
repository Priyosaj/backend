using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.OrderDTOs;

public class OrderToReturnDto : IMapFrom<Order>
{
    public int Id { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public ShippingAddress ShipToAddress { get; set; } = null!;
    public string DeliveryMethod { get; set; }
    public decimal ShippingPrice { get; set; }
    public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public string Status { get; set; }
}