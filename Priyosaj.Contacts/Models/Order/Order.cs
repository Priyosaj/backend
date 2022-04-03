using Priyosaj.Contacts.Models.Identity;

namespace Priyosaj.Contacts.Models.Order;

public class Order : BaseRepositoryItem
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public ICollection<OrderedItem> OrderedItems { get; set; }

    public string AddressId;
    public Address Address;
    
    public OrderStatus Status { get; set; }
}