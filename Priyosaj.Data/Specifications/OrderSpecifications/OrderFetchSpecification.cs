using Microsoft.EntityFrameworkCore;
using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.Interfaces;

namespace Priyosaj.Data.Specifications.OrderSpecifications;

public class OrderFetchByCustomerIdSpecification : ABaseSpecification<Order>
{
    public OrderFetchByCustomerIdSpecification(Guid customerId)
        : base(o => o.Customer.Id == customerId)
    {
        AddInclude(x => x.Include(o => o.OrderedItems));
        AddInclude(x => x.Include(o => o.DeliveryMethod));
        AddInclude(x => x.Include(o => o.Customer));
        AddOrderByDescending(o => o.ModifiedAt);
    }
}

public class OrderFetchByIdSpecification : ABaseSpecification<Order>
{
    public OrderFetchByIdSpecification(Guid orderId)
        : base(o => o.Id == orderId)
    {
        AddInclude(x => x.Include(o => o.OrderedItems));
        AddInclude(x => x.Include(o => o.DeliveryMethod));
        AddInclude(x => x.Include(o => o.Customer));
        AddOrderByDescending(o => o.ModifiedAt);
    }
}