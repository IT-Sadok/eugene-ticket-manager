namespace TicketService.Api.Helpers;

public static class KafkaConstants
{
    public const string TicketReservationRequestTopic = "ticket-reservation-request";
    public const string TicketServiceGroup = "ticket-service-group";
    public const string OrderUpdateEventTopic = "order-update-event";
}