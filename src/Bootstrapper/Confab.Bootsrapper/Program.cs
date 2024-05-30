var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.MapGet("/", async context => { await context.Response.WriteAsync("Confab API!"); });

var task = app.RunAsync();

await task;