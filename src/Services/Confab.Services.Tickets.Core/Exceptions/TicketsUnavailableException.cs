using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Services.Tickets.Core.Exceptions;

public class TicketsUnavailableException : ConfabException
{
    public Guid ConferenceId { get; }

    public TicketsUnavailableException(Guid conferenceId)
        : base("There are no available tickets for the conference.")

    {
        ConferenceId = conferenceId;
    }
}