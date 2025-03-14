using EventService.Domain.RepositoryContracts.Events;
using EventService.Domain.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace EventService.Infrastructure.Database.Repositories;

public class EventsRepository : EventServiceEventServiceDbRepository<Event>, IEventsRepository
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