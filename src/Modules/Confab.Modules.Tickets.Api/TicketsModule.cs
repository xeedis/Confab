using Confab.Modules.Tickets.Core;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Tickets.Api;

public class TicketsModule : IModule
{
    public const string BasePath = "tickets-module";
    public string Name => "Tickets";
    public string Path => BasePath;
    
    public IEnumerable<string> Policies { get; } = new[] { "tickets" };
    
    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}