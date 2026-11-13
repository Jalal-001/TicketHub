using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace TicketHub.Api.ExceptionHandlers
{
    internal sealed class ValidationExceptionHandler(IProblemDetailsService problemDetailsService,
                                                     ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                logger.LogWarning("Validation failed: {@Errors}", errors);

                return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    Exception = exception,
                    ProblemDetails = new HttpValidationProblemDetails(errors)
                    {
                        Title = "One or more validation errors occurred.",
                        Type = validationException.GetType().Name,
                    }
                });
            }
            return false;
        }
    }
}
