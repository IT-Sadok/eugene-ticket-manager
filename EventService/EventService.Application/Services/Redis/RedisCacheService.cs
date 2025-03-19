using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace EventService.Application.Services.Redis;

public class RedisCacheService(IDistributedCache cache) : IRedisCacheService
{
    public async Task<T?> GetCachedData<T>(string key)
    {
        var cachedData = await cache.GetStringAsync(key);
        return cachedData is not null ? JsonSerializer.Deserialize<T>(cachedData) : default;
    }

    public async Task SetCachedData<T>(string key, T data, int expirationMinutes = 10)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes)
        };

        var serializedData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(key, serializedData, options);
    }

    public async Task RemoveCachedData(string key)
    {
        await cache.RemoveAsync(key);
    }
}