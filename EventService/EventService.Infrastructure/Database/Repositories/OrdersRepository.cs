using EventService.Domain.RepositoryContracts.Orders;
using EventService.Domain.RepositoryModels;
using MongoDB.Driver;

namespace EventService.Infrastructure.Database.Repositories;

public class OrdersRepository : EventServiceDbRepository<Order>, IOrdersRepository
{
    public OrdersRepository(IMongoDatabase database)
        : base(database, "orders")
    {
    }

    public async Task UpdateOrdersStatusAsync(List<Order> orders)
    {
        var tasks = orders.Select(order =>
        {
            var filter = Builders<Order>.Filter.Eq(o => o.TicketId, order.TicketId);
            var update = Builders<Order>.Update.Set(o => o.Status, order.Status);
            return _collection.UpdateOneAsync(filter, update);
        });

        await Task.WhenAll(tasks);
    }

    public async Task<List<Order>> GetOrdersByTicketIdsAsync(List<Guid> ticketIds)
    {
        var filter = Builders<Order>.Filter.In(o => o.TicketId, ticketIds);
        return await _collection.Find(filter).ToListAsync();
    }
}