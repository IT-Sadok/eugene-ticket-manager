using MassTransit;
using TicketService.Domain.Messages;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Domain.RepositoryModels.Enums;

namespace TicketService.Application.Consumers;

public class TicketAvailabilityEventConsumer(ITicketsRepository ticketsRepository)
    : IConsumer<Batch<ReserveTicketEvent>>
{
    public async Task Consume(ConsumeContext<Batch<ReserveTicketEvent>> context)
    {
        foreach (var messageContext in context.Message)
        {
            var message = messageContext.Message;
            var ticket = await ticketsRepository.GetById(message.TicketId);
            if (ticket == null) return;
            if (ticket.Status != TicketStatus.Available) return;
            await ticketsRepository.ReserveTicketAsync(ticket.Id);
        }
    }
}