using System.Reflection;
using EventService.Application.Services;
using EventService.Application.Services.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        services.AddSingleton<IRedisCacheService, RedisCacheService>();
    }
}