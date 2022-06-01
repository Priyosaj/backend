using Priyosaj.Contacts.Entities.IdentityEntities;
using Priyosaj.Contacts.Entities.OrderEntities;

namespace Priyosaj.Contacts.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}