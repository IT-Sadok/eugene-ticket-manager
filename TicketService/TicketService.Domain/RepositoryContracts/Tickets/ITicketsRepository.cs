using TicketService.Domain.RepositoryModels;

namespace TicketService.Domain.RepositoryContracts.Tickets;

public interface ITicketsRepository: ITicketServiceDbRepository<Ticket>
{
    Task<List<Ticket>> GetAllAsync();
    Task CreateTicketsAsync(List<Ticket> ticket);
    Task<List<Ticket>> GetAllByEventIdAsync(Guid eventId);
    Task<Ticket?> GetWithHighestPlaceNumberByEventId(Guid eventId);
}