using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Modules.Agendas.Infrastructure.DAL;
using Confab.Modules.Agendas.Infrastructure.DAL.Repositories;
using Confab.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Confab.Modules.Agendas.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddPostgres<AgendasDbContext>()
            .AddScoped<ISubmissionRepository, SubmissionRepository>();
    }
}