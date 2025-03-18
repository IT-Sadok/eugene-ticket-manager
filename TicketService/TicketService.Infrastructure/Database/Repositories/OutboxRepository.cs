using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TicketService.Domain.Outbox;
using TicketService.Domain.RepositoryContracts.Outbox;

namespace TicketService.Infrastructure.Database.Repositories;

public class OutboxRepository(TicketServiceDbContext dbContext)
    : TicketServiceDbRepository<OutboxMessage>(dbContext), IOutboxRepository
{
    public async Task InsertOutboxMessageAsync<T>(T message) where T : notnull
    {
        var outbox = new OutboxMessage()
        {
            Type = message.GetType().FullName,
            OccurredOnUtc = DateTime.UtcNow,
            Data = JsonSerializer.Serialize(message)
        };
        await AddAsync(outbox);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<OutboxMessage>> GetOccuredMessagesByTypeAsync<TMessage>() where TMessage : notnull
    {
        return await GetAllRecords().Where(x => x.Type == typeof(TMessage).FullName && x.ProcessedOnUtc == null)
            .OrderBy(x => x.OccurredOnUtc).ToListAsync();
    }

    public async Task UpdateMessageProcessedDateAsync(Guid messageId, DateTime processedDate)
    {
        await GetAllRecords().Where(x => x.Id == messageId)
            .ExecuteUpdateAsync(x => x.SetProperty(c => c.ProcessedOnUtc, processedDate));
    }
}