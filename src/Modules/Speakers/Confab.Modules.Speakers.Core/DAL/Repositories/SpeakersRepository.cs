using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Speakers.Core.DAL.Repositories;

internal sealed class SpeakersRepository(SpeakersDbContext context) : ISpeakersRepository
{
    private readonly DbSet<Speaker> _speakers = context.Speakers;

    public async Task<IReadOnlyList<Speaker>> BrowseAsync()
        => await _speakers.ToListAsync();

    public async Task<Speaker> GetAsync(Guid id)
        => await _speakers.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<bool> ExistsAsync(Guid id)
        => await _speakers.AnyAsync(s => s.Id == id);

    public async Task AddAsync(Speaker speaker)
    {
        await _speakers.AddAsync(speaker);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Speaker speaker)
    {
        _speakers.Update(speaker);
        await context.SaveChangesAsync();
    }
}