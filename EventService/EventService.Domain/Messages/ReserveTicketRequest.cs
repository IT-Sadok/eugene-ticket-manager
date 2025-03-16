namespace EventService.Domain.Messages;

public record ReserveTicketRequest
{
    public Guid TicketId { get; init; }
}