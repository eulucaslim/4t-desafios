using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Exceptions;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Error: {Message}, Exception Got at: {Time}", exception.Message, DateTime.UtcNow);
        
        httpContext.Response.StatusCode = exception switch
        {
            EntityAlreadyExists => StatusCodes.Status409Conflict,
            EntityNotFound => StatusCodes.Status404NotFound,
            InvalidAnsCode or InvalidCpfException or InvalidLengthException => StatusCodes.Status422UnprocessableEntity,
            ApplicationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        var details = new ProblemDetails
        {
            Detail = exception.StackTrace,
            Title = exception.Message,
            Instance = httpContext.Request.Path,
            Status = httpContext.Response.StatusCode
        };
        
        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);
        return true;
    }
}