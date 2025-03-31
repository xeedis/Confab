using Confab.Shared.Abstractions.Events;

namespace Confab.Services.Tickets.Core.Events.External;

public record ConferenceCreated(Guid Id, string Name, 
    int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;