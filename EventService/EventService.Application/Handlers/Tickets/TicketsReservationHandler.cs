using EventService.Application.Commands;
using EventService.Domain.Messages;
using MassTransit;
using MediatR;

namespace EventService.Application.Handlers.Tickets;

public class TicketsReservationHandler(ITopicProducer<ReserveTicketEvent> producer)
    : IRequestHandler<ReserveTicketCommand>
{
    public async Task Handle(ReserveTicketCommand request, CancellationToken cancellationToken)
    {
        await producer.Produce(new ReserveTicketEvent
        {
            TicketId = request.TicketId
        }, cancellationToken);
    }
}