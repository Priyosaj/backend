using Priyosaj.Core.Entities.BasketEntities;

namespace Priyosaj.Core.Interfaces.Repositories;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(Guid basketId);
    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(Guid basketId);
}