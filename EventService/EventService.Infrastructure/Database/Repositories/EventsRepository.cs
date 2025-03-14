using EventService.Domain.RepositoryContracts.Events;
using EventService.Domain.RepositoryModels;
using MongoDB.Driver;

namespace EventService.Infrastructure.Database.Repositories;

public class EventsRepository : EventServiceDbRepository<Event>, IEventsRepository
{
    public EventsRepository(IMongoDatabase database)
        : base(database, "events")
    {

    }
    public async Task<List<Event>> GetAllEvents()
    {
        return await GetAllRecordsAsync();
    }
}