﻿using Priyosaj.Contacts.Entities.Identity;
using Priyosaj.Contacts.Entities.Order;
using Priyosaj.Contacts.Interfaces.Repositories;
using Priyosaj.Contacts.Interfaces.Services;
using Priyosaj.Contacts.Specifications;

namespace Priyosaj.Business.Services;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _basketRepo = basketRepo;
    }

    public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid id, string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

        return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
    }

    public Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

        return await _unitOfWork.Repository<Order>().ListAllAsyncWithSpec(spec);
    }
}