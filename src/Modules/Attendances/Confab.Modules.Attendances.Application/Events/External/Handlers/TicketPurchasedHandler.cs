using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Attendances.Application.Events.External.Handlers;

internal sealed class TicketPurchasedHandler : IEventHandler<TicketPurchased>
{
    public TicketPurchasedHandler()
    {
        
    }
    public Task HandleAsync(TicketPurchased @event)
    {
        throw new NotImplementedException();
    }
}