using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Saga;

public static class Extensions
{
    public static IServiceCollection AddSaga(this IServiceCollection services)
    {
        services.AddSaga();
        return services;
    }
}