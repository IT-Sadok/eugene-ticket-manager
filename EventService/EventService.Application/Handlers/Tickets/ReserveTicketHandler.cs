using EventService.Application.Commands;
using EventService.Domain.Messages;
using MassTransit;
using MediatR;

namespace EventService.Application.Handlers.Tickets;

public class ReserveTicketHandler(ITopicProducer<ReserveTicketRequest> producer)
    : IRequestHandler<ReserveTicketCommand>
{
    public async Task Handle(ReserveTicketCommand request, CancellationToken cancellationToken)
    {
        await producer.Produce(new ReserveTicketRequest
        {
            TicketId = request.TicketId
        }, cancellationToken);
    }
}