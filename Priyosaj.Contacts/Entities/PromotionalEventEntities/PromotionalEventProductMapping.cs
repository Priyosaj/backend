using Priyosaj.Contacts.Entities.ProductEntities;

namespace Priyosaj.Contacts.Entities.PromotionalEventEntities;
public class PromotionalEventProductMapping
{
    public Guid PromotionalEventId { get; set; }
    public PromotionalEvent PromotionalEvent { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Decimal? EventDiscountPrice { get; set; } = null;
}