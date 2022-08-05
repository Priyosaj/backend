namespace Priyosaj.Core.Entities.BasketEntities;

public class BasketItem
{
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; }
    public string PictureUrl { get; set; } = string.Empty;
}