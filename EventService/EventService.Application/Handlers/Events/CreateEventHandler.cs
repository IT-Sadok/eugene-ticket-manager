using EventService.Domain.RepositoryContracts.Events;
using EventService.Domain.RepositoryModels;
using MediatR;
using TicketService.Commands;

namespace TicketService.Handlers.Events;

public class CreateEventHandler(IEventsRepository eventsRepository) : IRequestHandler<CreateEventCommand>
{
    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventRepo = new Event()
            { Title = request.Title, Description = request.Description, StartedAt = request.StartedAt };
        await eventsRepository.InsertAsync(eventRepo);
    }
}