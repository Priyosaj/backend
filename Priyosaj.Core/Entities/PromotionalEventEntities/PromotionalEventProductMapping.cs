using Priyosaj.Core.Entities.ProductEntities;

namespace Priyosaj.Core.Entities.PromotionalEventEntities;
public class PromotionalEventProductMapping
{
    public Guid PromotionalEventId { get; set; }
    public PromotionalEvent PromotionalEvent { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Decimal? EventDiscountPrice { get; set; } = null;
}