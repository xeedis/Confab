using Confab.Modules.Conferences.Api;
using Confab.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure()
    .AddConferences();

var app = builder.Build();

app.UseInfrastructure();
app.Run();