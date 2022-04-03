namespace Priyosaj.Contacts.Models.Order;

public class OrderedItem : BaseRepositoryItem
{
    public string ProductTitle { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
    
    public string PictureUrl { get; set; } = string.Empty;
    
    // public decimal RegularPrice { get; set; }
    //
    // public decimal DiscountPrice { get; set; }
    
    public decimal SellingPrice { get; set; }
}