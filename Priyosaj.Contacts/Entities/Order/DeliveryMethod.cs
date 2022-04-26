﻿namespace Priyosaj.Contacts.Entities.Order;

public class DeliveryMethod : BaseRepositoryItem
{
    public string ShortName { get; set; } = string.Empty;
    public string DeliveryTime { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}