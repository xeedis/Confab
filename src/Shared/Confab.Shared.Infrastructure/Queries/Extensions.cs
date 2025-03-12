using System.Reflection;
using Confab.Shared.Abstractions.Events;
using Confab.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Shared.Infrastructure.Queries;

internal static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                .WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
         
        
        return services;
    }
}