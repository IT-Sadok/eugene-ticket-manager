using MediatR;

namespace EventService.Application.Commands;

public class ReserveTicketCommand: IRequest
{
    public Guid TicketId { get; set; }
}