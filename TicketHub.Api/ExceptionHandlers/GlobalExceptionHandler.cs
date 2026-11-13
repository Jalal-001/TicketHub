using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TicketHub.Api.ExceptionHandlers
{
    internal sealed class GlobalExceptionHandler(
                            IProblemDetailsService problemDetailsService,
                            ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("An unhandled exception occurred: {@Exception}", exception);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = "InternalServerError",
                    Title = "An error occured",
                    Detail = "An unexpected error occurred. Please try again later or contact support if the problem persists."
                }
            });
        }
    }
}
