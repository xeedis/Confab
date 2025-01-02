using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Tickets.Core.Exceptions;

public class TooManyTicketsException(Guid conferenceId)
    : ConfabException("Too many tickets would be generated for the conference.")
{
    public Guid ConferenceId { get; } = conferenceId;
}