using MediatR;
using TicketService.Domain.RepositoryModels;

namespace TicketService.Application.Queries;

public class GetTicketsByEventIdQuery: IRequest<List<Ticket>>
{
    public Guid EventId { get; set; }
}