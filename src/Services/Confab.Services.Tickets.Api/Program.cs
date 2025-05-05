using Confab.Services.Tickets.Core;
using Confab.Services.Tickets.Core.Events.External;
using Convey;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCore();

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseConvey();
app.UseRabbitMq()
    .SubscribeEvent<ConferenceCreated>();
app.Run();