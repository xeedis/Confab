using Confab.Services.Tickets.Core.Entities;
using Confab.Services.Tickets.Core.Repositories;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace Confab.Services.Tickets.Core.Events.External.Handlers;

public class ConferenceCreatedHandler(
    IConferenceRepository repository,
    ILogger<ConferenceCreatedHandler> logger)
    : IEventHandler<ConferenceCreated>
{
    public async Task HandleAsync(ConferenceCreated @event, CancellationToken cancellationToken)
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