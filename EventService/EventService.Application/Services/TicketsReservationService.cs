using EventService.Domain.Messages;
using MassTransit;

namespace EventService.Application.Services;

public class TicketsReservationService(ITopicProducer<ReserveTicketRequest> producer) : ITicketsReservationService
{
    public async Task ReserveTicket(Guid ticketId, CancellationToken cancellationToken = default)
    {
        await producer.Produce(new ReserveTicketRequest
        {
            TicketId = ticketId
        }, cancellationToken);
    }
}