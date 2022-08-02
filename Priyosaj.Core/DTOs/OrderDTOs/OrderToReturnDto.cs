﻿using Priyosaj.Core.Entities.OrderEntities;

namespace Priyosaj.Core.DTOs.OrderDTOs;

public class OrderToReturnDto
{
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public ShippingAddress ShipToAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public decimal ShippingPrice { get; set; }
    public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public string Status { get; set; }
}