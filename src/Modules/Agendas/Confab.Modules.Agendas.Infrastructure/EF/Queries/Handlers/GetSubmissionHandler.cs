using Confab.Modules.Agendas.Application.Submissions.DTO;
using Confab.Modules.Agendas.Application.Submissions.Queries;
using Confab.Modules.Agendas.Domain.Submissions.Entities;
using Confab.Modules.Agendas.Infrastructure.EF;
using Confab.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace Confab.Modules.Agendas.Infrastructure.Queries.Handlers;

internal sealed class GetSubmissionHandler : IQueryHandler<GetSubmission, SubmissionDto>
{
    private readonly DbSet<Submission> _submissions;
    public GetSubmissionHandler(AgendasDbContext context)
    {
        _submissions = context.Submissions;
    }

    public async Task<SubmissionDto> HandleAsync(GetSubmission query)
        => await _submissions
            .AsNoTracking()
            .Where(x => x.Id.Equals(query.Id))
            .Include(x => x.Speakers)
            .Select(x => new SubmissionDto
            {
                Id = x.Id,
                ConferenceId = x.ConferenceId,
                Title = x.Title,
                Description = x.Description,
                Level = x.Level,
                Status = x.Status,
                Tags = x.Tags,
                Speakers = x.Speakers.Select(s => new SpeakerDto
                {
                    Id = s.Id,
                    FullName = s.FullName
                }).ToList()
            })
            .SingleOrDefaultAsync();
}