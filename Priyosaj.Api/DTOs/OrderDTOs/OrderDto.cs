using Priyosaj.Api.DTOs.AddressDTOs;

namespace Priyosaj.Api.DTOs.OrderDTOs;

public class OrderDto
{
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
}