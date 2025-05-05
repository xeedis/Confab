using System.Runtime.CompilerServices;
using Confab.Services.Tickets.Core.DAL;
using Confab.Services.Tickets.Core.DAL.Repositories;
using Confab.Services.Tickets.Core.Repositories;
using Confab.Services.Tickets.Core.Services;
using Confab.Shared.Abstractions.Storage;
using Confab.Shared.Abstractions.Time;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Auth;
using Confab.Shared.Infrastructure.Commands;
using Confab.Shared.Infrastructure.Contexts;
using Confab.Shared.Infrastructure.Events;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Kernel;
using Confab.Shared.Infrastructure.Messaging;
using Confab.Shared.Infrastructure.Modules;
using Confab.Shared.Infrastructure.Postgres;
using Confab.Shared.Infrastructure.Queries;
using Confab.Shared.Infrastructure.Services;
using Confab.Shared.Infrastructure.Storage;
using Confab.Shared.Infrastructure.Time;
using Convey;
using Convey.CQRS.Events;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Services.Tickets.Api")]
namespace Confab.Services.Tickets.Core;

internal static class Extensions
{
    internal static IServiceCollection AddCore(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
       
        services.AddMemoryCache();
        services.AddSingleton<IContextFactory, ContextFactory>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IRequestStorage, RequestStorage>();
        services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
        services.AddModuleRequests(assemblies);
        services.AddSwaggerGen();
        services.AddErrorHandling();
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        services.AddDomainEvents(assemblies);
        services.AddEvents(assemblies);
        services.AddMessaging();
        services.AddPostgres();
        services.AddTransactionalDecorators();
        services.AddSingleton<IClock, UtcClock>();
        services.AddAuth();
        services.AddHostedService<AppInitializer>();
        services.AddControllers();

        services.AddConvey()
            .AddRabbitMq()
            .AddEventHandlers()
            .AddInMemoryEventDispatcher()
            .Build();
        
        return services
            .AddScoped<ITicketService, TicketService>()
            .AddScoped<ITicketSaleService, TicketSaleService>()
            .AddScoped<IConferenceRepository, ConferenceRepository>()
            .AddScoped<ITicketRepository, TicketRepository>()
            .AddScoped<ITicketSaleRepository, TicketSaleRepository>()
            .AddSingleton<ITicketGenerator, TicketGenerator>()
            .AddPostgres<TicketsDbContext>();
    }
}