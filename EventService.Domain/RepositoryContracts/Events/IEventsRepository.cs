using EventService.Domain.RepositoryModels;

namespace EventService.Domain.RepositoryContracts.Events;

public interface IEventsRepository: IDbRepository<Event>
{
    Task<List<Event>> GetAllEvents();
}