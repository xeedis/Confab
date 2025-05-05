using Confab.Shared.Abstractions.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Modules;

internal sealed class ModuleSubscriber : IModuleSubscriber
{
    private readonly IModuleRegistry _registry;
    private readonly IServiceProvider _serviceProvider;

    public ModuleSubscriber(IModuleRegistry registry, IServiceProvider serviceProvider)
    {
        _registry = registry;
        _serviceProvider = serviceProvider;
    }
    public IModuleSubscriber Subscribe<TRequest, TResponse>(string path,
        Func<TRequest, IServiceProvider, Task<TResponse>> action)
        where TRequest : class where TResponse : class
    {
        _registry.AddRequestAction(path, typeof(TRequest), typeof(TResponse),
            async request =>
            {
                using var scope = _serviceProvider.CreateScope();
                return await action((TRequest) request, scope.ServiceProvider);
            });

        return this;
    }
}