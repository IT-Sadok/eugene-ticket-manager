using EventService.Domain.RepositoryContracts.Events;
using EventService.Infrastructure.Database;
using EventService.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventService.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EventServiceContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("EventServiceDbConnection"));
        });

        services.AddScoped<IEventsRepository, EventsRepository>();
    }
}