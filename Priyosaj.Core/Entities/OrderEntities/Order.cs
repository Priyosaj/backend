using Priyosaj.Core.Entities.IdentityEntities;

namespace Priyosaj.Core.Entities.OrderEntities;

public class Order : ABaseEntity
{
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }

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