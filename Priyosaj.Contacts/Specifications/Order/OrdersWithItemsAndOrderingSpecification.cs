﻿using Microsoft.EntityFrameworkCore;
using Priyosaj.Contacts.Entities.Order;

namespace Priyosaj.Contacts.Specifications;

public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
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