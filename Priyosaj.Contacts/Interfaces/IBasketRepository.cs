using Priyosaj.Contacts.Models;

namespace Priyosaj.Contacts.Interfaces;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(Guid basketId);
    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(Guid basketId);
}