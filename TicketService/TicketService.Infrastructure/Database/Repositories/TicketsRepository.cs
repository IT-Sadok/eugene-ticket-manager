using Microsoft.EntityFrameworkCore;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Domain.RepositoryModels;

namespace TicketService.Infrastructure.Database.Repositories;

public class TicketsRepository(TicketServiceDbContext dbContext) : TicketServiceDbRepository<Ticket>(dbContext), ITicketsRepository
{
    public async Task<List<Ticket>> GetAllAsync()
    {
        return await GetAllRecords().OrderBy(x => x.EventId).ThenBy(x => x.PlaceNumber).ToListAsync();
    }

    public async Task CreateTicketsAsync(List<Ticket> ticket)
    {
        await AddRangeAsync(ticket);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Ticket>> GetAllByEventIdAsync(Guid eventId)
    {
        return await GetAllRecords().Where(x => x.EventId == eventId).OrderBy(x => x.PlaceNumber).ToListAsync();
    }

    public async Task<Ticket?> GetWithHighestPlaceNumberByEventId(Guid eventId)
    {
        return await GetAllRecords().Where(x => x.EventId == eventId).OrderByDescending(x => x.PlaceNumber)
            .FirstOrDefaultAsync();
    }
}