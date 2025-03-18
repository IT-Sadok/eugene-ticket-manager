using System.Data;
using MassTransit;
using TicketService.Domain.Messages;
using TicketService.Domain.RepositoryContracts.Outbox;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Domain.RepositoryModels.Enums;

namespace TicketService.Application.Consumers;

public class ReserveTicketEventConsumer(ITicketsRepository ticketsRepository, ITopicProducer<OrderUpdateEvent> producer, IOutboxRepository outboxRepository)
    : IConsumer<Batch<ReserveTicketEvent>>
{
    public async Task Consume(ConsumeContext<Batch<ReserveTicketEvent>> context)
    {
        foreach (var messageContext in context.Message)
        {
            var message = messageContext.Message;
            var ticket = await ticketsRepository.GetById(message.TicketId);

            if (ticket == null)
            {
                await producer.Produce(new OrderUpdateEvent() { TicketId = message.TicketId, IsFailure = true });
                continue;
            }

            if (ticket.Status != TicketStatus.Available)
            {
                await producer.Produce(new OrderUpdateEvent() { TicketId = message.TicketId, IsFailure = true });
                continue;
            }
            await using var transaction = await ticketsRepository.BeginTransactionAsync(IsolationLevel.Serializable);

            await ticketsRepository.ReserveTicketAsync(ticket.Id);
            await outboxRepository.InsertOutboxMessageAsync(new OrderUpdateEvent()
                { TicketId = message.TicketId, IsFailure = false });

            await transaction.CommitAsync();
        }
    }
}