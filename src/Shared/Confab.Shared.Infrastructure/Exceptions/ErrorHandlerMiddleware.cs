using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Exceptions;

internal class ErrorHandlerMiddleware(IExceptionCompositionRoot exceptionCompositionRoot
    ,ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
{
    private readonly IExceptionCompositionRoot _exceptionCompositionRoot = exceptionCompositionRoot;
    private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandlerErrorAsync(context, exception);
        }
    }

    private async Task HandlerErrorAsync(HttpContext context, Exception exception)
    {
        var errorResponse = _exceptionCompositionRoot.Map(exception);
        context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        var response = errorResponse?.Response;
        if (response is null)
        {
            return;
        }
        
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}