using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Domain.Submissions.Repositories;
using Confab.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.EF.Repositories;

public class SubmissionRepository: ISubmissionRepository
{
    private readonly AgendasDbContext _dbContext;
    private readonly DbSet<Submission> _submissions;

    public SubmissionRepository(AgendasDbContext dbContext)
    {
        _dbContext = dbContext;
        _submissions = dbContext.Submissions;
    }

    public async Task<Submission> GetAsync(AggregateId id)
        => await _submissions
            .Include(x => x.Speakers)
            .SingleOrDefaultAsync(x => x.Id.Equals(id));

    public async Task AddAsync(Submission entity)
    {
        _submissions.Add(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Submission entity)
    {
        _submissions.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
