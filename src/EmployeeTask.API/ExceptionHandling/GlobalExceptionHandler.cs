using EmployeeTask.API.ExceptionHandling.CustomExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTask.API.ExceptionHandling
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler 
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
        {
            var (statusCode, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found"),
                ValidationException => (StatusCodes.Status400BadRequest, "Bad Request"),
                OperationCanceledException => (StatusCodes.Status499ClientClosedRequest, "Request Canceled"),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
            };

            _logger.LogError(exception, "Error occurred: {Message}. Mapping to {StatusCode}", exception.Message, statusCode);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);

            return true;
        }
    }
}
