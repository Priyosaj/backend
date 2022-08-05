using Priyosaj.Core.Entities.BasketEntities;

namespace Priyosaj.Core.Interfaces.Repositories;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetUserBasketAsync(Guid userId);
    Task<CustomerBasket?> UpdateUserBasketAsync(CustomerBasket basket);
    Task<bool> DeleteUserBasketAsync(Guid basketId);
}