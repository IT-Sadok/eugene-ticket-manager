namespace TicketService.Api.Requests;

public class CreateTicketsRequest
{
    public Guid EventId { get; set; }
    public int Quantity { get; set; }
}