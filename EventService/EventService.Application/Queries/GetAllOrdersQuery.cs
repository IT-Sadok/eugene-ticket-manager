using EventService.Domain.RepositoryModels;
using MediatR;

namespace EventService.Application.Queries;

public class GetAllOrdersQuery: IRequest<List<Order>>
{
    
}