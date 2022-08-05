using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.OrderEntities;

namespace Priyosaj.Core.Specifications.OrderSpecifications;

public class OrdersWithItemsAndOrderingSpecification : ABaseSpecification<Order>
{
    public OrdersWithItemsAndOrderingSpecification(string email) 
        // : base(o => o.BuyerEmail == email)
    {
        AddInclude(x => x.Include(o => o.OrderedItems));
        AddInclude(x => x.Include(o => o.DeliveryMethod));
        AddOrderByDescending(o => o.ModifiedAt);
    }

    public OrdersWithItemsAndOrderingSpecification(Guid id, string email) 
        : base(o => o.Id == id)
    {
        AddInclude(x => x.Include(o => o.OrderedItems));
        AddInclude(x => x.Include(o => o.DeliveryMethod));
    }
}