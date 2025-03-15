using MediatR;

namespace TicketService.Application.Commands;

public class CreateTicketsCommand: IRequest
{
    public Guid EventId { get; set; }
    public int Quantity { get; set; }
}