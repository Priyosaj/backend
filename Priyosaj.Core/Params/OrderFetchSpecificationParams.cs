using Priyosaj.Core.Entities.OrderEntities;

namespace Priyosaj.Core.Params;
public class OrderFetchSpecificationParams
{
    // Guid? customerId, OrderStatus status, DeliveryMethod deliveryMethod, DateTime startDate, DateTime endDate
    public Guid? CustomerId { get; set; } = null;
    public OrderStatus? Status { get; set; } = null;
    
    public string? DeliveryMethod { get; set; } = null;

    public DateTime? StartDate { get; set; } = null;

    public DateTime? EndDate { get; set; } = null;
    private const int MaxPageSize = 50;

    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Min(MaxPageSize, PageSize);
    }
}
