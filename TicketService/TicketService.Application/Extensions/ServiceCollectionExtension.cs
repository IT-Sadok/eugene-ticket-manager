﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TicketService.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
    }
}