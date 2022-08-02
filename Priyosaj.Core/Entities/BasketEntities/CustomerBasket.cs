namespace Priyosaj.Core.Entities.BasketEntities;

public class CustomerBasket
{
    public CustomerBasket()
    {
    }

    public CustomerBasket(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
}