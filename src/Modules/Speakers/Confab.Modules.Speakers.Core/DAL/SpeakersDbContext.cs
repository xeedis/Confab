using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Speakers.Core.DAL;

internal class SpeakersDbContext(DbContextOptions<SpeakersDbContext> options) : DbContext(options)
{
    public DbSet<Speaker> Speakers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("speakers");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpeakersDbContext).Assembly);
    }
    
}