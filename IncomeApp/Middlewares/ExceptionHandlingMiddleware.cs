using IncomeApp.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace IncomeApp.Middlewares;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        switch (exception)
        {
            case DomainException:
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = $"Bad request: {exception.Message}, Time of occurrence {DateTime.UtcNow}"
                }, cancellationToken);
                return true;
            }
            case RecordNotFoundException:
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = $"Record not found: {exception.Message}, Time of occurrence {DateTime.UtcNow}"
                }, cancellationToken);
                return true;
            }
            case UnauthorizedAccessException:
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Message = $"Unauthorized access: {exception.Message}, Time of occurrence {DateTime.UtcNow}"
                }, cancellationToken);
                return true;
            }
            default:
                return false;
        }
    }
}