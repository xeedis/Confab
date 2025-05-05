using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Conferences.Core.Exceptions;

internal class CannotDeleteHConferenceException(Guid id)
    : ConfabException($"Conference with ID: '{id}' cannot be deleted.")
{
    public Guid Id { get; } = id;
}