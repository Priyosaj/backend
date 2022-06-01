using Priyosaj.Contacts.Entities.IdentityEntities;

namespace Priyosaj.Contacts.Entities.OrderEntities;

public class Order : BaseRepositoryItem
{
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;

    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public ShippingAddress ShippingAddress { get; set; } = null!;

    public ICollection<OrderedItem> OrderedItems { get; set; } = null!;

    public decimal SubTotal { get; set; }

    public decimal GetTotal()
    {
        // return SubTotal;
        return SubTotal + DeliveryMethod.Price;
    }
}