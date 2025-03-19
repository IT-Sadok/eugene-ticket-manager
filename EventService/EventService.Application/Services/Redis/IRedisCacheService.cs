namespace EventService.Application.Services.Redis;

public interface IRedisCacheService
{
    T? GetCachedData<T>(string key);
    void SetCachedData<T>(string key, T data, int expirationMinutes = 10);
    void RemoveCachedData(string key);
}