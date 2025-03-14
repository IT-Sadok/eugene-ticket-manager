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
        var ticketWithHighestPlaceNumber = await ticketsRepository.GetWithHighestPlaceNumberByEventId(command.EventId);
        var startCount = ticketWithHighestPlaceNumber?.PlaceNumber ?? 0;
        var tickets = new List<Ticket>();
        for (var i = 1; i <= command.Quantity; i++)
        {
            tickets.Add(new Ticket()
                { Status = TicketStatus.Available, PlaceNumber = i + startCount, EventId = command.EventId });
        }

        if (tickets.Any())
            await ticketsRepository.CreateTicketsAsync(tickets);
    }
}