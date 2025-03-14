using MediatR;
using TicketService.Application.Queries;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Domain.RepositoryModels;

namespace TicketService.Application.Handlers.Tickets;

public class GetAllTicketsHandler(ITicketsRepository ticketsRepository)
    : IRequestHandler<GetAllTicketsQuery, List<Ticket>>
{
    public async Task<List<Ticket>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        return await ticketsRepository.GetAllAsync();
    }
}