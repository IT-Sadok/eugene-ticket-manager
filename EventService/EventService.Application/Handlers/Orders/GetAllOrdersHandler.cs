using EventService.Application.Queries;
using EventService.Application.Services;
using EventService.Application.Services.Redis;
using EventService.Domain.RepositoryContracts.Orders;
using EventService.Domain.RepositoryModels;
using MediatR;

namespace EventService.Application.Handlers.Orders;

public class GetAllOrdersHandler(IOrdersRepository repository, IRedisCacheService redisCache) : IRequestHandler<GetAllOrdersQuery, List<Order>>
{

    public async Task<List<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var cachedProducts = redisCache.GetCachedData<List<Order>>(RedisKeyConstants.OrdersKey);
        if (cachedProducts is not null)
            return cachedProducts;

        var orders = await repository.GetAllRecordsAsync();
        redisCache.SetCachedData(RedisKeyConstants.OrdersKey, orders);
        return orders;
    }
}