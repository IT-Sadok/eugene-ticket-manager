using MassTransit;
using TicketService.Domain.Messages;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Domain.RepositoryModels.Enums;

namespace TicketService.Application.Consumers;

public class TicketAvailabilityRequestConsumer(ITicketsRepository ticketsRepository) : IConsumer<ReserveTicketRequest>
{
    public async Task Consume(ConsumeContext<ReserveTicketRequest> context)
    {
        var message = context.Message;
        var ticket = await ticketsRepository.GetById(message.TicketId);
        if (ticket == null) return;
        if (ticket.Status != TicketStatus.Available) return;
        await ticketsRepository.ReserveTicketAsync(ticket.Id);
    }
}