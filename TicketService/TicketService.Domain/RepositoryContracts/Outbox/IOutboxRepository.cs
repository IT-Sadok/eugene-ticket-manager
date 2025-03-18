using TicketService.Domain.Outbox;

namespace TicketService.Domain.RepositoryContracts.Outbox;

public interface IOutboxRepository: ITicketServiceDbRepository<OutboxMessage>
{
    Task InsertOutboxMessageAsync<T>(T message) where T : notnull;
    Task<List<OutboxMessage>> GetOccuredMessagesByTypeAsync<TMessage>() where TMessage : notnull;
    Task UpdateMessageProcessedDateAsync(Guid messageId, DateTime processedDate);
}