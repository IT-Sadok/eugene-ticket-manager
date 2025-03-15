using Microsoft.EntityFrameworkCore.Storage;
using TicketService.Domain.RepositoryContracts;

namespace TicketService.Infrastructure.Database;

public class TicketServiceTransaction(IDbContextTransaction transaction) : ITransaction
{
    public Task CommitAsync() => transaction.CommitAsync();

    public Task RollbackAsync() => transaction.RollbackAsync();

    public ValueTask DisposeAsync() => transaction.DisposeAsync();
}