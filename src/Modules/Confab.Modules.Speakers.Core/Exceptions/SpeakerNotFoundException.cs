using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Speakers.Core.Exceptions;

public class SpeakerNotFoundException(Guid id)
    : ConfabException($"Speaker with ID: '{id}' cannot be found.")
{
    public Guid Id { get; } = id;
}