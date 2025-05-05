using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Users.Core.Exceptions;

internal sealed class EmailInUseException() : ConfabException("Email is already in use.");