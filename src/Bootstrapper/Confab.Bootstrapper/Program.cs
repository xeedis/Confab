using System.Reflection;
using Confab.Bootstrapper;
using Confab.Shared.Abstractions.Modules;
using Confab.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

IList<Assembly> _assemblies = ModuleLoader.LoadAllAssemblies();
IList<IModule> _modules = ModuleLoader.LoadModules(_assemblies);

var configuration = builder.Configuration;

builder.Services.AddInfrastructure(configuration);
foreach (var module in _modules)
{
    module.Register(builder.Services);
}

var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();

app.UseInfrastructure();
foreach (var module in _modules)
{
    module.Use(app);
}

logger.LogInformation($"Modules: {string.Join(", ", _modules.Select(x => x.Name))}");

app.MapControllers();
app.MapGet("/", () => "Confab API!");

_assemblies.Clear();
_modules.Clear();

app.Run();