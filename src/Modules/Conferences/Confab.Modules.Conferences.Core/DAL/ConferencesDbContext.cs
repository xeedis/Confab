using Confab.Modules.Conferences.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL;

internal class ConferencesDbContext(DbContextOptions<ConferencesDbContext> options) : DbContext(options)
{
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<Host> Hosts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("conferences");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConferencesDbContext).Assembly);
    }
}