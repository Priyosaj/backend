using Priyosaj.Contacts.Entities.Order;

namespace Priyosaj.Contacts.Specifications;

public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
{
    public OrdersWithItemsAndOrderingSpecification(string email) 
        // : base(o => o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderedItems);
        AddInclude(o => o.DeliveryMethod);
        AddOrderByDescending(o => o.ModifiedAt);
    }

    public OrdersWithItemsAndOrderingSpecification(Guid id, string email) 
        : base(o => o.Id == id)
    {
        AddInclude(o => o.OrderedItems);
        AddInclude(o => o.DeliveryMethod);
    }
}