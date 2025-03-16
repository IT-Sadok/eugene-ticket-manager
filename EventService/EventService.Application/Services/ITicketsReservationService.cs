namespace EventService.Application.Services;

public interface ITicketsReservationService
{
    Task ReserveTicket(Guid ticketId, CancellationToken cancellationToken = default);
}