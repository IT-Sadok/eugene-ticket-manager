using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketService.Commands;
using TicketService.Queries;

namespace EventService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly ISender _sender;

    public EventsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> ListEvents()
    {
        var response = await _sender.Send(new GetAllEventsQuery());
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventCommand command)
    {
        await _sender.Send(command);
        return Created();
    }
}