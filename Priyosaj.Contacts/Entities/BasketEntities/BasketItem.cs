namespace Priyosaj.Contacts.Entities.BasketEntities;

public class BasketItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; }
    public string PictureUrl { get; set; }
}