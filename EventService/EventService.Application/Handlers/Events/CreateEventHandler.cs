﻿using EventService.Application.Commands;
using EventService.Domain.RepositoryContracts.Events;
using EventService.Domain.RepositoryModels;
using MediatR;

namespace EventService.Application.Handlers.Events;

public class CreateEventHandler(IEventsRepository eventsRepository) : IRequestHandler<CreateEventCommand>
{
    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventRepo = new Event()
            { Title = request.Title, Description = request.Description, StartedAt = request.StartedAt };
        await eventsRepository.CreateAsync(eventRepo);
    }
}