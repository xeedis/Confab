using System.Runtime.CompilerServices;
using Confab.Services.Tickets.Core.DAL;
using Confab.Services.Tickets.Core.DAL.Repositories;
using Confab.Services.Tickets.Core.Repositories;
using Confab.Services.Tickets.Core.Services;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Confab.Services.Tickets.Api")]
namespace Confab.Services.Tickets.Core;

internal static class Extensions
{
    internal static IServiceCollection AddCore(this IServiceCollection services)
    {
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