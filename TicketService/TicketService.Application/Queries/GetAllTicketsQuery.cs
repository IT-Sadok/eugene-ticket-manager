using MediatR;
using TicketService.Domain.RepositoryModels;

namespace TicketService.Application.Queries;

public class GetAllTicketsQuery: IRequest<List<Ticket>>
{
    
}