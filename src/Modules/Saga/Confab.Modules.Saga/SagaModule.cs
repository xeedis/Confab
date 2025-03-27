using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Services;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Saga;

internal class SagaModule : IModule
{
    public const string BasePath = "saga-module";
    public string Name => "Saga";
    public string Path => BasePath;
    
    public void Register(IServiceCollection services)
    {
        services.AddSaga();
    }

    public void Use(IApplicationBuilder app)
    {
        app
            .UseModuleRequest()
            .Subscribe<SpeakerDto, object>("speakers/create", async (dto, sp) =>
            {
                var service = sp.GetRequiredService<ISpeakersService>();
                await service.CreateAsync(dto);
                return null;
            });
    }
}