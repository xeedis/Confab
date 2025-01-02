using System.Collections.Generic;
using Confab.Modules.Speakers.Core;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Speakers.Api;

internal sealed class SpeakersModule : IModule
{
    public const string BasePath = "speakers-module";
    public string Name => "Speakers";
    public string Path => BasePath;
    public IEnumerable<string> Policies { get; } = ["speakers"];
    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}