using Confab.Shared.Abstractions.Modules;

namespace Confab.Shared.Infrastructure.Modules;

internal sealed class ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
    : IModuleClient
{
    private readonly IModuleSerializer _moduleSerializer = moduleSerializer;

    public async Task PublishAsync(object message)
    {
        var key = message.GetType().Name;
        var registrations = moduleRegistry.GetBroadCastRegistrations(key);

        var tasks = new List<Task>();
        
        foreach (var registration in registrations)
        {
            var action = registration.Action;
            var receiverMessage = TranslateType(message, registration.ReceiverType);
            tasks.Add(action(receiverMessage));
        }
        
        await Task.WhenAll(tasks);
    }

    private object TranslateType(object value, Type type)
        => _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
}