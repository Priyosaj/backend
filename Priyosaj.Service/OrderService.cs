using AutoMapper;
using Priyosaj.Core.DTOs.OrderDTOs;
using Priyosaj.Core.Entities.OrderEntities;
using Priyosaj.Core.Entities.ProductEntities;
using Priyosaj.Core.Interfaces.Repositories;
using Priyosaj.Core.Interfaces.Services;
using Priyosaj.Core.Params;
using Priyosaj.Core.Utils;
using Priyosaj.Data.Specifications.OrderSpecifications;

namespace Priyosaj.Service;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _basketRepo = basketRepo;
    }

    #region AdminSection

    public async Task<IReadOnlyList<OrderToReturnDto>> GetAllOrdersAsync(OrderFetchSpecificationParams orderParams)
    {
        _currentUserService.ValidateIfEditor();

        var spec = new OrderFetchSpecification(orderParams);
        
        var orders = await _unitOfWork.Repository<Order>().ListAllAsyncWithSpec(spec);

        if (orders == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
    }

    public async Task<int> CountOrdersAsync(OrderFetchSpecificationParams orderParams)
    {
        var spec = new OrderFetchSpecification(orderParams);
        var count = await _unitOfWork.Repository<Order>().CountAsync(spec);
        return count;
    }

    public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid orderId)
    {
        _currentUserService.ValidateIfEditor();
        var spec = new OrderFetchByIdSpecification(orderId);

        var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<OrderToReturnDto>(order);
    }

    #endregion

    #region CustomerSection

    public async Task<OrderToReturnDto> CreateOrderAsync(CreateOrderRequestDto orderReq)
    {
        _currentUserService.ValidateIfCustomer();
        var userId = _currentUserService.UserId;

        var order = _mapper.Map<Order>(orderReq);

        var basket = await _basketRepo.GetUserBasketAsync(userId);

        if (basket == null || basket.Items.Count == 0)
        {
            throw new BadRequestException("No Item in the basket");
        }

        order.AppUserId = _currentUserService.UserId;
        order.Status = OrderStatus.Pending;
        foreach (var item in basket.Items)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);

            if (product == null)
            {
                basket.Items = basket.Items.Where(i => i.ProductId != item.ProductId).ToList();
                await _basketRepo.UpdateUserBasketAsync(basket);
                throw new BadRequestException(
                    "Non Existent Product Added to Basket. Product Has Been Removed From the Basket. Please Try Again!");
            }

            if (product.StockCount <= 0 || item.Quantity > product.StockCount)
            {
                await _basketRepo.DeleteUserBasketAsync(userId);
                throw new BadRequestException($"Sorry! {product.Title} is Out Of Stock!");
            }

            var orderedItem = new OrderedItem
            {
                ProductTitle = product.Title,
                Quantity = item.Quantity,
                PictureUrl = null,
                Product = product,
                SellingPrice = product.DiscountPrice ?? product.RegularPrice,
            };
            order.OrderedItems.Add(orderedItem);
        }

        var subTotal = order.OrderedItems.Select(o => o.SellingPrice).Sum();
        order.SubTotal = subTotal;

        _unitOfWork.Repository<Order>().Add(order);

        var isSuccess = (await _unitOfWork.Complete()) != 0;

        if (!isSuccess)
        {
            throw new Exception("Something went wrong!");
        }

        return _mapper.Map<OrderToReturnDto>(order);
    }

    public async Task<IReadOnlyList<OrderToReturnDto>> GetOrdersForUserAsync()
    {
        var customerId = _currentUserService.UserId;
        var spec = new OrderFetchByCustomerIdSpecification(customerId);

        var orders = await _unitOfWork.Repository<Order>().ListAllAsyncWithSpec(spec);

        return _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
    }

    public async Task<OrderToReturnDto> GetCustomerOrderByIdAsync(Guid orderId)
    {
        var spec = new OrderFetchByIdSpecification(orderId);

        var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null || order.AppUserId != _currentUserService.UserId)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<OrderToReturnDto>(order);
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }
    #endregion
}