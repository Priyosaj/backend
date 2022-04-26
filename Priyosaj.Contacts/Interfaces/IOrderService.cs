using Priyosaj.Contacts.Entities.Identity;
using Priyosaj.Contacts.Entities.Order;

namespace Priyosaj.Contacts.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}