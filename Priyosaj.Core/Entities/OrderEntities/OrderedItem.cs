namespace Priyosaj.Core.Entities.OrderEntities;

public class OrderedItem : ABaseEntity
{
    public string ProductTitle { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
    
    public string PictureUrl { get; set; } = string.Empty;
    
    // public decimal RegularPrice { get; set; }
    //
    // public decimal DiscountPrice { get; set; }
    
    public decimal SellingPrice { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}