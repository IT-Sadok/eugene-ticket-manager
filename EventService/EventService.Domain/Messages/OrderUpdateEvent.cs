namespace EventService.Domain.Messages;

public class OrderUpdateEvent
{
    public Guid TicketId { get; set; }
    public bool IsFailure { get; set; }
}