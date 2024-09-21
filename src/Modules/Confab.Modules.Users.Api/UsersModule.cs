using Confab.Modules.Users.Core;
using Confab.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Users.Api;

internal class UsersModule : IModule
{
    public const string BasePath = "users-module";

    public string Name => "Users";
    public string Path => BasePath;

    public IEnumerable<string> Policies { get; } = ["users"];
    
    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}