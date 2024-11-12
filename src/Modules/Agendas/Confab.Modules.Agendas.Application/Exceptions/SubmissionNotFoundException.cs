using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Exceptions;

public class SubmissionNotFoundException : ConfabException
{
    public Guid SubmissionId { get; }
    public SubmissionNotFoundException(Guid submissionId) : base($"Submission with id {submissionId} was not found.")
    {
        SubmissionId = submissionId;
    }
}