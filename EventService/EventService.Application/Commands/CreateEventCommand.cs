using MediatR;

namespace TicketService.Commands;

public class CreateEventCommand: IRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartedAt { get; set; }
}