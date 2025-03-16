using EventService.Api.Helpers;
using EventService.Application.Commands;
using EventService.Application.Queries;
using MediatR;

namespace EventService.Api.Controllers;

public static class EventEndpoints
{
    public static void MapEventEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(RouteConstants.EventsRoute, async (ISender sender) =>
        {
            var response = await sender.Send(new GetAllEventsQuery());
            return Results.Ok(response);
        });

        app.MapPost(RouteConstants.EventsRoute, async (ISender sender, CreateEventCommand command) =>
        {
            await sender.Send(command);
            return Results.Created();
        });

        app.MapPost($"{RouteConstants.EventsRoute}/tickets/{{id:guid}}", async (ISender sender, Guid id) =>
        {
            await sender.Send(new ReserveTicketCommand(){TicketId = id});
            return Results.Created();
        });
    }
}