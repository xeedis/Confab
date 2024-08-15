using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Confab.Shared.Infrastructure.Exceptions;

internal class ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
{
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
        var errorResponse = new { code = exception.GetType().Name, message = exception.Message };
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}