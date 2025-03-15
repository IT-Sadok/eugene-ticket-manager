namespace TicketService.Domain.RepositoryContracts;

public interface ITransaction: IAsyncDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}