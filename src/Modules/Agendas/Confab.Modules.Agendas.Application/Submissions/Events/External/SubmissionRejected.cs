using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Submissions.Events.External;

public record SubmissionRejected(Guid Id) : IEvent;