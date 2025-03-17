using EventService.Domain.Enums;
using EventService.Domain.Messages;
using EventService.Domain.RepositoryContracts.Orders;
using EventService.Domain.RepositoryModels;
using MassTransit;

namespace EventService.Application.Consumers;

public class OrderUpdateEventConsumer(IOrdersRepository ordersRepository) : IConsumer<Batch<OrderUpdateEvent>>
{
    public async Task Consume(ConsumeContext<Batch<OrderUpdateEvent>> context)
    {
        var ordersToUpdate = new List<Order>();
        foreach (var messageContext in context.Message)
        {
            var message = messageContext.Message;
            ordersToUpdate.Add(new Order()
            {
                TicketId = message.TicketId, Status = message.IsFailure ? OrderStatus.Failed : OrderStatus.Reserved
            });
        }

        if (ordersToUpdate.Any())
            await ordersRepository.UpdateOrdersStatusAsync(ordersToUpdate);
    }
}