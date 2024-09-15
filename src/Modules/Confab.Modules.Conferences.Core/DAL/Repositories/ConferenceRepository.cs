using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Conferences.Core.DAL.Repositories;

internal class ConferenceRepository(ConferencesDbContext dbContext) : IConferenceRepository
{
    private readonly DbSet<Conference> _conferences = dbContext.Conferences;
    public async Task<Conference> GetAsync(Guid id)
        => await _conferences
            .Include(x=>x.Host)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IReadOnlyList<Conference>> BrowseAsync()
        => await _conferences.Include(x=>x.Host)
            .ToListAsync();

    public async Task AddAsync(Conference conference)
    {
        await _conferences.AddAsync(conference);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Conference conference)
    {
        _conferences.Update(conference);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Conference conference)
    {
        _conferences.Remove(conference);
        await dbContext.SaveChangesAsync();    
    }
}