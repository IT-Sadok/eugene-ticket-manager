using TicketService.Domain.RepositoryModels;

namespace TicketService.Domain.Outbox;

public class OutboxMessage : BaseEntity
{
    public DateTime OccurredOnUtc { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTime? ProcessedOnUtc { get; set; }
}