using TicketService.Domain.RepositoryModels.Enums;

namespace TicketService.Domain.RepositoryModels;

public class Ticket : BaseEntity
{
    public int PlaceNumber { get; set; }
    public TicketStatus Status { get; set; }
    public Guid EventId { get; set; }
}