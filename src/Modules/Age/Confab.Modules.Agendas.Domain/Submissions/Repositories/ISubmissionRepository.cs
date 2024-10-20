using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Repositories;

public interface ISubmissionRepository
{
    Task<Submission> GetAsync(AggregateId id);
    Task AddASync();
    Task UpdateAsync(Submission submission);
}