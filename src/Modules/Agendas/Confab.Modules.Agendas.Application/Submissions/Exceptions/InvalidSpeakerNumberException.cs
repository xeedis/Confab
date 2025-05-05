using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Application.Submissions.Exceptions;

public class InvalidSpeakerNumberException : ConfabException
{
    public Guid SubmissionId { get; }
    
    public InvalidSpeakerNumberException(Guid submissionId) 
        : base($"Submission with ID: '{submissionId}' has invalid number of speakers.")
        => SubmissionId = submissionId;
}