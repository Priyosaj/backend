using Priyosaj.Contacts.Entities.BasketEntities;

namespace Priyosaj.Contacts.Interfaces.Repositories;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(Guid basketId);
    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(Guid basketId);
}