namespace Confab.Shared.Infrastructure.Modules;

public sealed class ModuleBroadcastRegistration(Type receiverType, Func<object, Task> action)
{
    public Type ReceiverType { get; set; } = receiverType;
    public Func<object,Task> Action { get; set; } = action;
    public string Key => ReceiverType.Name;
}