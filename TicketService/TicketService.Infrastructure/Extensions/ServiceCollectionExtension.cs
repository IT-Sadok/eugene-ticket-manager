using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketService.Domain.RepositoryContracts;
using TicketService.Domain.RepositoryContracts.Outbox;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Infrastructure.Database;
using TicketService.Infrastructure.Database.Repositories;

namespace TicketService.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TicketServiceDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("TicketServiceDbConnection"));
        });
        services.AddScoped<ITicketsRepository, TicketsRepository>()
            .AddScoped<IOutboxRepository, OutboxRepository>();
    }
}