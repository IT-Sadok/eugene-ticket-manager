using System.Data;
using System.Text.Json;
using MassTransit;
using Quartz;
using TicketService.Domain.Messages;
using TicketService.Domain.RepositoryContracts.Outbox;

namespace TicketService.Application.QurtzJobs;

public class OrderUpdateJob(IOutboxRepository outboxRepository, ITopicProducer<OrderUpdateEvent> producer) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var dtNow = DateTime.UtcNow;
        await using var transaction = await outboxRepository.BeginTransactionAsync(IsolationLevel.Serializable);
        var outboxMessages = await outboxRepository.GetOccuredMessagesByTypeAsync<OrderUpdateEvent>();
        foreach (var outboxMessage in outboxMessages)
        {
            var orderUpdateEvent = JsonSerializer.Deserialize<OrderUpdateEvent>(outboxMessage.Data);
            await producer.Produce(orderUpdateEvent);
            await outboxRepository.UpdateMessageProcessedDateAsync(outboxMessage.Id, dtNow);
            await transaction.CommitAsync();
        }
    }
}