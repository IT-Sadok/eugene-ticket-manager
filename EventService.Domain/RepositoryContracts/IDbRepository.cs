using EventService.Domain.RepositoryModels;

namespace EventService.Domain.RepositoryContracts;

public interface IDbRepository<T> where T : BaseEntity
{
    Task<T> InsertAsync(T entity);
    Task DeleteAsync(T entity);
    IQueryable<T> GetAllRecords();
    Task UpdateAsync(T entity);
}