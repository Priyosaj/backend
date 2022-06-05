using Priyosaj.Contacts.DTOs.AddressDTOs;

namespace Priyosaj.Contacts.DTOs.OrderDTOs;

public class OrderDto
{
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
}