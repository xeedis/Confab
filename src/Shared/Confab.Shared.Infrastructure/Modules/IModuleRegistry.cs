namespace Confab.Shared.Infrastructure.Modules;

public interface IModuleRegistry
{
    IEnumerable<ModuleBroadcastRegistration> GetBroadCastRegistrations(string key);
    void AddBroadcastAction(Type requestType, Func<object, Task> action);
}