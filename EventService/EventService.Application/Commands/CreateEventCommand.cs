using MediatR;

namespace EventService.Application.Commands;

public class CreateEventCommand: IRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartedAt { get; set; }
}