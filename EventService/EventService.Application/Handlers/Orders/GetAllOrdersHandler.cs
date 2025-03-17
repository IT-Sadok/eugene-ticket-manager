using EventService.Application.Queries;
using EventService.Domain.RepositoryContracts.Orders;
using EventService.Domain.RepositoryModels;
using MediatR;

namespace EventService.Application.Handlers.Orders;

public class GetAllOrdersHandler(IOrdersRepository repository) : IRequestHandler<GetAllOrdersQuery, List<Order>>
{
    public async Task<List<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetAllRecordsAsync();
    }
}