using EventService.Domain.RepositoryContracts;
using EventService.Domain.RepositoryModels;

namespace EventService.Infrastructure.Database;

public class DbRepository<T>(EventServiceContext context) : IDbRepository<T>
    where T : BaseEntity
{
    public async Task<T> InsertAsync(T entity)
    {
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public IQueryable<T> GetAllRecords() => context.Set<T>().AsQueryable();

    public async Task UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }
}