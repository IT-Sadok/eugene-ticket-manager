using EventService.Domain.RepositoryContracts.Events;
using EventService.Domain.RepositoryModels;
using MediatR;
using TicketService.Queries;

namespace TicketService.Handlers.Events;

public class GetAllEventsHandler(IEventsRepository eventsRepository) : IRequestHandler<GetAllEventsQuery, List<Event>>
{
    public async Task<List<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        return await eventsRepository.GetAllEvents();
    }
}