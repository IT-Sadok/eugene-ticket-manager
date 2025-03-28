﻿using EventService.Domain.RepositoryModels;

namespace EventService.Domain.RepositoryContracts.Orders;

public interface IOrdersRepository: IEventServiceDbRepository<Order>
{
    Task UpdateOrdersStatusAsync(List<Order> orders);
    Task<List<Order>> GetOrdersByTicketIdsAsync(List<Guid> ticketIds);
}