using System.Data;
using TicketService.Domain.RepositoryModels;

namespace TicketService.Domain.RepositoryContracts;

public interface ITicketServiceDbRepository<T> where T: BaseEntity
{
    Task AddAsync(T entity);
    IQueryable<T> GetAllRecords();
    void UpdateAsync(T entity);
    Task AddRangeAsync(List<T> entities);
    Task<ITransaction> BeginTransactionAsync(IsolationLevel isolationLevel);
}