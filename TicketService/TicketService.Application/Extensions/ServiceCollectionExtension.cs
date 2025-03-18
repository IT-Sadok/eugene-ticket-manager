using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using TicketService.Application.QurtzJobs;

namespace TicketService.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        services.AddQuartz(o =>
        {
            var orderUpdateJob = JobKey.Create(nameof(OrderUpdateJob));

            o.AddJob<OrderUpdateJob>(orderUpdateJob).AddTrigger(t=>t.ForJob(orderUpdateJob).WithCronSchedule("5,15,25,35,45,55 * * * * ?"));
        });
        services.AddQuartzHostedService();
    }
}