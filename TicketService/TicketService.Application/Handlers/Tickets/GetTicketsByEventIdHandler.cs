using MediatR;
using TicketService.Application.Queries;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Domain.RepositoryModels;

namespace TicketService.Application.Handlers.Tickets;

public class GetTicketsByEventIdHandler(ITicketsRepository ticketsRepository)
    : IRequestHandler<GetTicketsByEventIdQuery, List<Ticket>>
{
    public async Task<List<Ticket>> Handle(GetTicketsByEventIdQuery request, CancellationToken cancellationToken)
    {
        return await ticketsRepository.GetAllByEventIdAsync(request.EventId);
    }
}