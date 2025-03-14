using EventService.Domain.RepositoryContracts;
using EventService.Domain.RepositoryContracts.Events;
using EventService.Infrastructure.Database.MongoInitializer;
using EventService.Infrastructure.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EventService.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoDatabase>(_ =>
            new MongoClient(configuration.GetConnectionString("EventServiceDbConnection")).GetDatabase(
                configuration["DatabaseName"]));
        services.AddScoped<IEventsRepository, EventsRepository>()
            .AddSingleton<IMongoDbInitializer, MongoDbInitializer>();
    }
}