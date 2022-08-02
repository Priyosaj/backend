namespace Priyosaj.Core.Entities.OrderEntities;

public class DeliveryMethod : ABaseEntity
{
    public string ShortName { get; set; } = string.Empty;
    public string DeliveryTime { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}