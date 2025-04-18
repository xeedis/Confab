using Confab.Modules.Agendas.Application;
using Confab.Modules.Agendas.Application.Agendas.DTO;
using Confab.Modules.Agendas.Application.Agendas.Queries;
using Confab.Modules.Agendas.Domain;
using Confab.Modules.Agendas.Infrastructure;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Abstractions.Queries;
using Confab.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Agendas.Api;

internal class AgendasModule : IModule
{
    public const string BasePath = "agendas-module";
    public string Name => "Agendas";
    public string Path => BasePath;
    public IEnumerable<string> Policies { get; } =
    [
        "agendas", "submissions"
    ];
    
    public void Register(IServiceCollection services)
    {
        services.AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequest()
            .Subscribe<GetRegularAgendaSlot, RegularAgendaSlotDto>("agendas/slots/regular/get", (query, sp) =>
                sp.GetRequiredService<IQueryDispatcher>().QueryAsync(query));
    }
}