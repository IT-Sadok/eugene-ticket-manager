using EventService.Domain.RepositoryContracts.Events;
using EventService.Domain.RepositoryModels;
using Microsoft.EntityFrameworkCore;

namespace EventService.Infrastructure.Database.Repositories;

public class EventsRepository(EventServiceContext context) : DbRepository<Event>(context), IEventsRepository
{
    public async Task<List<Event>> GetAllEvents()
    {
        return await GetAllRecords().ToListAsync();
    }
}