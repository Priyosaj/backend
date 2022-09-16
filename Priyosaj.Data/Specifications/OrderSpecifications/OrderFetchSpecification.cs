using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.Interfaces;
using Priyosaj.Core.Params;

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

public class OrderFetchSpecification : ABaseSpecification<Order>
{
    public OrderFetchSpecification(OrderFetchSpecificationParams orderFetchParams)
    : base(o => 
    (orderFetchParams.CustomerId == null || o.AppUserId == orderFetchParams.CustomerId)
    && (!orderFetchParams.Status.HasValue || o.Status == orderFetchParams.Status)
    && (orderFetchParams.DeliveryMethod == null || o.DeliveryMethod.ShortName == orderFetchParams.DeliveryMethod)
    && (!orderFetchParams.StartDate.HasValue || o.CreatedAt >= orderFetchParams.StartDate)
    && (!orderFetchParams.EndDate.HasValue || o.CreatedAt <= orderFetchParams.EndDate))
    {
        AddInclude(x => x.Include(o => o.OrderedItems));
        AddInclude(x => x.Include(o => o.DeliveryMethod));
        AddInclude(x => x.Include(o => o.Customer));
        AddOrderByDescending(o => o.ModifiedAt);
    }
}