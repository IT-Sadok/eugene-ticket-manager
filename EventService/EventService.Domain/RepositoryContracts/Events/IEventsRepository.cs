using EventService.Domain.RepositoryModels;

namespace EventService.Domain.RepositoryContracts.Events;

public interface IEventsRepository: IEventServiceDbRepository<Event>
{
    Task<List<Event>> GetAllEvents();
}