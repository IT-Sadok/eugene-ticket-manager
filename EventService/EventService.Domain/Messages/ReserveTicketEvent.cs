namespace EventService.Domain.Messages;

public record ReserveTicketEvent
{
    public Guid TicketId { get; init; }
}