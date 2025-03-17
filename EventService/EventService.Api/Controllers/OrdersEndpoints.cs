using EventService.Api.Helpers;
using EventService.Application.Queries;
using MediatR;

namespace EventService.Api.Controllers;

public static class OrdersEndpoints
{
    public static void MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(RouteConstants.OrdersRoute, async (ISender sender) =>
        {
            var response = await sender.Send(new GetAllOrdersQuery());
            return Results.Ok(response);
        });
    }
}