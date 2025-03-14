using TicketService.Domain.RepositoryContracts;
using TicketService.Domain.RepositoryModels;

namespace TicketService.Infrastructure.Database;

public class TicketServiceDbRepository<T>(TicketServiceDbContext dbContext) : ITicketServiceDbRepository<T>
    where T : BaseEntity
{
    public async Task AddAsync(T entity)
    {
        await dbContext.AddAsync(entity);
    }

    public async Task AddRangeAsync(List<T> entities)
    {
        await dbContext.AddRangeAsync(entities);
    }

    public IQueryable<T> GetAllRecords()
    {
        return dbContext.Set<T>().AsQueryable();
    }

    public void UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
    }
}