namespace Priyosaj.Contacts.Interfaces.Services;
public interface IResponseCacheService
{
    Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
    Task<string?> GetCachedResponse(string cacheKey);
}