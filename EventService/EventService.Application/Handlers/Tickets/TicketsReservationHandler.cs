using EventService.Application.Commands;
using EventService.Domain.Enums;
using EventService.Domain.Messages;
using EventService.Domain.RepositoryContracts.Orders;
using EventService.Domain.RepositoryModels;
using MassTransit;
using MediatR;

namespace EventService.Application.Handlers.Tickets;

public class TicketsReservationHandler(ITopicProducer<ReserveTicketEvent> producer, IOrdersRepository ordersRepository)
    : IRequestHandler<ReserveTicketCommand>
{
    public async Task Handle(ReserveTicketCommand request, CancellationToken cancellationToken)
    {
        var order = new Order() { TicketId = request.TicketId, Status = OrderStatus.Pending };
        await ordersRepository.CreateAsync(order);
        await producer.Produce(new ReserveTicketEvent
        {
            TicketId = request.TicketId
        }, cancellationToken);
    }
}