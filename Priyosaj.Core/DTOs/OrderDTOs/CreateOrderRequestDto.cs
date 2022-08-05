using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.MapperProfile;

namespace Priyosaj.Core.DTOs.OrderDTOs;

public class CreateOrderRequestDto : IMapFrom<Order>
{
    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public ShippingAddressDto ShippingAddress { get; set; } = null!;
}

public class ShippingAddressDto : IMapFrom<ShippingAddress>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string HouseNo { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}