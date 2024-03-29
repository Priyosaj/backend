using System.Text.Json;
using Priyosaj.Core.Entities.BasketEntities;
using Priyosaj.Core.Interfaces.Repositories;
using StackExchange.Redis;

namespace Priyosaj.Data;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<bool> DeleteUserBasketAsync(Guid basketId)
    {
        return await _database.KeyDeleteAsync(basketId.ToString());
    }

    public async Task<CustomerBasket?> GetUserBasketAsync(Guid userId)
    {
        var data = await _database.StringGetAsync(userId.ToString());

        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket?> UpdateUserBasketAsync(CustomerBasket basket)
    {
        var created = await _database.StringSetAsync(basket.Id.ToString(), JsonSerializer.Serialize(basket),
            TimeSpan.FromDays(30));

        if (!created) return null;

        return await GetUserBasketAsync(basket.Id);
    }
}