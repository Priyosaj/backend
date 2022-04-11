using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Contacts.Models.Order;

public class Order : BaseRepositoryItem
{
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    
    public ShippingAddress ShippingAddress;

    public ICollection<OrderedItem> OrderedItems { get; set; }

    public decimal SubTotal { get; set; }

    public decimal GetTotal()
    {
        return SubTotal;
        // return SubTotal + DeleveryMethod.Price;
    }
}