using Priyosaj.Core.Entities.ProductEntities;

namespace Priyosaj.Core.Entities.OrderEntities;

public class OrderedItem : ABaseEntity
{
    public string ProductTitle { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public string PictureUrl { get; set; } = string.Empty;

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public decimal SellingPrice { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
}