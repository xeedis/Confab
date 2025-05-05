using Confab.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Events;

internal sealed class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{

    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
    {
        Console.WriteLine($"[DEBUG] Publishing event of type: {typeof(TEvent).FullName}");
        using var scope = serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        
        Console.WriteLine($"[DISPATCHER] Dispatching {typeof(TEvent).FullName} to handlers: {string.Join(", ", handlers.Select(h => h.GetType().Name))}");
        
        var tasks = handlers.Select(h => h.HandleAsync(@event));
        
        await Task.WhenAll(tasks);
    }
}