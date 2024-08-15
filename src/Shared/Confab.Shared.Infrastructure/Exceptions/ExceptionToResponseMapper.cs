using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Shared.Infrastructure.Exceptions;

internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
    {
        throw new NotImplementedException();
    }
}