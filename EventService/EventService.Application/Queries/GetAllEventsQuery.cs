using EventService.Domain.RepositoryModels;
using MediatR;

namespace EventService.Application.Queries;

public class GetAllEventsQuery : IRequest<List<Event>>
{
}