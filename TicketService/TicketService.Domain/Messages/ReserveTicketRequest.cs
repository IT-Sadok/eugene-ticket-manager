namespace TicketService.Domain.Messages;

public record ReserveTicketRequest
{
    public Guid TicketId { get; init; }
}