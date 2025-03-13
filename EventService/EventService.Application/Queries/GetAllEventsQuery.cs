using EventService.Domain.RepositoryModels;
using MediatR;

namespace TicketService.Queries;

public class GetAllEventsQuery : IRequest<List<Event>>
{
}