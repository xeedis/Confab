using Confab.Modules.Tickets.Core.Entities;

namespace Confab.Modules.Tickets.Core.Repositories;

public interface IConferenceRepository
{
    Task<Conference> GetAsync(Guid id);
    Task AddAsync(Conference conference);
    Task UpdateAsync(Conference conference);
    Task DeleteAsync(Conference conference);
}