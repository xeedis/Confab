using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories;

internal class HostRepository(ConferencesDbContext dbContext) : IHostRepository
{
    private readonly DbSet<Host> _hosts = dbContext.Hosts;
    public async Task<Host> GetAsync(Guid id)
        => await _hosts
            .Include(x=>x.Conferences)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IReadOnlyList<Host>> BrowseAsync()
        => await _hosts.Include(x=>x.Conferences)
            .ToListAsync();

    public async Task AddAsync(Host host)
    {
        await _hosts.AddAsync(host);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Host host)
    {
        _hosts.Update(host);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Host host)
    {
        _hosts.Remove(host);
        await dbContext.SaveChangesAsync();    
    }
}