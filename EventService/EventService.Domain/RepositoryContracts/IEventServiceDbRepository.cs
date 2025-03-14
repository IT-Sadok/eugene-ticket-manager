using EventService.Domain.RepositoryModels;

namespace EventService.Domain.RepositoryContracts;

public interface IEventServiceDbRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task<List<T>> GetAllRecordsAsync();
}