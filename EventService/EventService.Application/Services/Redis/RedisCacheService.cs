using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace EventService.Application.Services.Redis;

public class RedisCacheService(IDistributedCache cache) : IRedisCacheService
{
    public T? GetCachedData<T>(string key)
    {
        var cachedData = cache.GetString(key);
        return cachedData is not null ? JsonSerializer.Deserialize<T>(cachedData) : default;
    }

    public void SetCachedData<T>(string key, T data, int expirationMinutes = 10)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes)
        };

        var serializedData = JsonSerializer.Serialize(data);
        cache.SetString(key, serializedData, options);
    }

    public void RemoveCachedData(string key)
    {
        cache.Remove(key);
    }
}