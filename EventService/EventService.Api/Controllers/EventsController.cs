using MediatR;
using TicketService.Commands;
using TicketService.Queries;

namespace EventService.Api.Controllers;

public static class EventEndpoints
{
    public static void MapEventEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/events", async (ISender sender) =>
        {
            var response = await sender.Send(new GetAllEventsQuery());
            return Results.Ok(response);
        });

        app.MapPost("/events", async (ISender sender, CreateEventCommand command) =>
        {
            await sender.Send(command);
            return Results.Created();
        });
    }
}