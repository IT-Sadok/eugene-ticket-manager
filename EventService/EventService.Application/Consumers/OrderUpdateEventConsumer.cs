using EventService.Application.Services;
using EventService.Application.Services.Redis;
using EventService.Domain.Enums;
using EventService.Domain.Messages;
using EventService.Domain.RepositoryContracts.Orders;
using EventService.Domain.RepositoryModels;
using MassTransit;

namespace EventService.Application.Consumers;

public class OrderUpdateEventConsumer(IOrdersRepository ordersRepository, IRedisCacheService redisCache) : IConsumer<Batch<OrderUpdateEvent>>
{
    public async Task Consume(ConsumeContext<Batch<OrderUpdateEvent>> context)
    {
        var ticketIds = context.Message.Select(m => m.Message.TicketId).Distinct().ToList();
        var orders = await ordersRepository.GetOrdersByTicketIdsAsync(ticketIds);
        var ordersDictionary = orders.ToDictionary(o => o.TicketId);

        var ordersToUpdate = new List<Order>();
        foreach (var messageContext in context.Message)
        {
            var message = messageContext.Message;

            if (!ordersDictionary.TryGetValue(message.TicketId, out var order)) continue;
            var newStatus = message.IsFailure ? OrderStatus.Failed : OrderStatus.Reserved;

            if (order.Status == newStatus) continue;
            order.Status = newStatus;
            ordersToUpdate.Add(order);
        }

        if (ordersToUpdate.Any())
        {
            await ordersRepository.UpdateOrdersStatusAsync(ordersToUpdate);
            await redisCache.RemoveCachedDataAsync(RedisKeyConstants.OrdersKey);
        }
    }
}