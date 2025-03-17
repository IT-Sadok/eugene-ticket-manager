using MassTransit;
using TicketService.Domain.Messages;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Domain.RepositoryModels.Enums;

namespace TicketService.Application.Consumers;

public class ReserveTicketEventConsumer(ITicketsRepository ticketsRepository, ITopicProducer<OrderUpdateEvent> producer)
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

            await ticketsRepository.ReserveTicketAsync(ticket.Id);
            await producer.Produce(new OrderUpdateEvent() { TicketId = message.TicketId, IsFailure = false });
        }
    }
}