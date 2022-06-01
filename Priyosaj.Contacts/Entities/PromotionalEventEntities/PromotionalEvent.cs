namespace Priyosaj.Contacts.Entities.PromotionalEventEntities;
public class PromotionalEvent : BaseRepositoryItem
{
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime StartingTime { get; set; }
    public DateTime EndingTime { get; set; }
    public ICollection<PromotionalEventProductMapping> PromotionalEventProductMappings { get; set; }
}