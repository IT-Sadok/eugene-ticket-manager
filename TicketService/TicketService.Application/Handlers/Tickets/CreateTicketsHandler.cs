using System.Data;
using MediatR;
using TicketService.Application.Commands;
using TicketService.Domain.RepositoryContracts.Tickets;
using TicketService.Domain.RepositoryModels;
using TicketService.Domain.RepositoryModels.Enums;

namespace TicketService.Application.Handlers.Tickets;

public class CreateTicketsHandler(ITicketsRepository ticketsRepository) : IRequestHandler<CreateTicketsCommand>
{
    public async Task Handle(CreateTicketsCommand command, CancellationToken cancellationToken)
    {
        await using var transaction = await ticketsRepository.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            var ticketWithHighestPlaceNumber =
                await ticketsRepository.GetWithHighestPlaceNumberByEventId(command.EventId);
            var startCount = ticketWithHighestPlaceNumber?.PlaceNumber ?? 0;
            var tickets = Enumerable.Range(1, command.Quantity).Select(i => new Ticket
            {
                Status = TicketStatus.Available,
                PlaceNumber = i + startCount,
                EventId = command.EventId
            }).ToList();

            if (tickets.Any())
            {
                await ticketsRepository.CreateTicketsAsync(tickets);
                await transaction.CommitAsync();
            }

        }
        catch
        {
            await transaction.RollbackAsync();
        }
    }
}