using Priyosaj.Core.DTOs.AddressDTOs;

namespace Priyosaj.Core.DTOs.OrderDTOs;

public class OrderDto
{
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
}