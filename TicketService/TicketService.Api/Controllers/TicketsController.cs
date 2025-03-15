using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketService.Api.Helpers;
using TicketService.Api.Requests;
using TicketService.Application.Commands;
using TicketService.Application.Queries;

namespace TicketService.Api.Controllers;

public static class TicketsEndpoint
{
    public static void MapTicketsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(RouteConstants.TicketsRoute, async (ISender sender) =>
        {
            var response = await sender.Send(new GetAllTicketsQuery());
            return Results.Ok(response);
        });

        app.MapPost(RouteConstants.TicketsRoute, async (ISender sender, CreateTicketsRequest  command) =>
        {
            await sender.Send(new CreateTicketsCommand(){EventId = command.EventId, Quantity = command.Quantity});
            return Results.Created();
        });

        app.MapGet($"events/{{id:guid}}/{RouteConstants.TicketsRoute}", async (Guid id, ISender sender) =>
        {
            var response = await sender.Send(new GetTicketsByEventIdQuery() { EventId = id });
            return Results.Ok(response);
        });
    }
}