namespace Priyosaj.Core.Entities.PromotionalEventEntities;
public class PromotionalEvent : ABaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime StartingTime { get; set; }
    public DateTime EndingTime { get; set; }
    public ICollection<PromotionalEventProductMapping> PromotionalEventProductMappings { get; set; }
}