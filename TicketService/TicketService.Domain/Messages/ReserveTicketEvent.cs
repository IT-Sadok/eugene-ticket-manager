namespace TicketService.Domain.Messages;

public record ReserveTicketEvent
{
    public Guid TicketId { get; init; }
}