using EventService.Application.Queries;
using EventService.Domain.RepositoryContracts.Events;
using EventService.Domain.RepositoryModels;
using MediatR;

namespace EventService.Application.Handlers.Events;

public class GetAllEventsHandler(IEventsRepository eventsRepository) : IRequestHandler<GetAllEventsQuery, List<Event>>
{
    public async Task<List<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        return await eventsRepository.GetAllEvents();
    }
}