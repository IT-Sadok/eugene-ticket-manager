namespace EventService.Application.Services.Redis;

public interface IRedisCacheService
{
    Task<T?> GetCachedData<T>(string key);
    Task SetCachedData<T>(string key, T data, int expirationMinutes = 10);
    Task RemoveCachedData(string key);
}