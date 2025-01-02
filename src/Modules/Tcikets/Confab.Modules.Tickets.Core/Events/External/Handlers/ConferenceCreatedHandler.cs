using Confab.Modules.Tickets.Core.Entities;
using Confab.Modules.Tickets.Core.Repositories;
using Confab.Shared.Abstractions.Events;
using Microsoft.Extensions.Logging;

namespace Confab.Modules.Tickets.Core.Events.External.Handlers;

public class ConferenceCreatedHandler(
    IConferenceRepository repository,
    ILogger<ConferenceCreatedHandler> logger)
    : IEventHandler<ConferenceCreated>
{
    public async Task HandleAsync(ConferenceCreated @event)
    {
        var conference = new Conference
        {
            Id = @event.Id,
            Name = @event.Name,
            ParticipantsLimit = @event.ParticipantsLimit,
            From = @event.From,
            To = @event.To,
        };
        
        await repository.AddAsync(conference);
        logger.LogInformation($"Added conference with ID: '{@event.Id}'");
    }
}