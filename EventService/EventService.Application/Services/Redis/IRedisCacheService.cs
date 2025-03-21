namespace EventService.Application.Services.Redis;

public interface IRedisCacheService
{
    Task<T?> GetCachedDataAsync<T>(string key);
    Task SetCachedDataAsync<T>(string key, T data, int expirationMinutes = 10);
    Task RemoveCachedDataAsync(string key);
}