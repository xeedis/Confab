using Confab.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Events;

internal sealed class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
    {
        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

        var tasks = handlers.Select(h => h.HandleAsync(@event));
        
        await Task.WhenAll(tasks);
    }
}