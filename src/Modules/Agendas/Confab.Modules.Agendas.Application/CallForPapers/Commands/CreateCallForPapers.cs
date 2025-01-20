using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.CallForPapers.Commands;

public record CreateCallForPapers(DateTime From, DateTime To, Guid ConferenceId) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
};
