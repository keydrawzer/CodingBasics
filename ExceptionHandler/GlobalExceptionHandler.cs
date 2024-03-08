using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        string? traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        LogException(exception, traceId, _logger);

        var (statusCode, title) = MapException(exception);

        await Results.Problem(
            title: title,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                {"traceId", traceId}
            }
        ).ExecuteAsync(httpContext);

        return true;
    }

    private static (int statusCode, string title) MapException(Exception exception)
    {
        return exception switch
        {
            BadHttpRequestException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error. We are working on it.")
        };
    }

    private static void LogException(Exception exception, string? traceId, ILogger<GlobalExceptionHandler> logger)
    {
        if (exception is BadHttpRequestException)
        {
            logger.LogWarning(
                exception,
                "\tTime: {ExceptionTime}\n\tTraceID: {TraceID}\n\tExceptionType: {ExceptionType}\n\tMessage: {Message}\n",
                DateTime.Now,
                traceId,
                exception.GetType().ToString(),
                exception.Message
            );
        }
        else
        {
            logger.LogError(
                exception,
                "\tTime: {ExceptionTime}\n\tTraceID: {TraceID}\n\tExceptionType: {ExceptionType}\n\tMessage: {Message}\n",
                DateTime.Now,
                traceId,
                exception.GetType().ToString(),
                exception.Message
            );
        }
    }
}
