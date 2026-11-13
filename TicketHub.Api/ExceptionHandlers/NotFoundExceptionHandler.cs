using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketHub.Application.Exceptions;

namespace TicketHub.Api.ExceptionHandlers
{
    internal sealed class NotFoundExceptionHandler(IProblemDetailsService problemDetailsService,
                                                   ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is NotFoundException notFoundException)
            {
                logger.LogWarning("A NotFoundException occurred: {@NotFoundMessage}", notFoundException.Message);
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    Exception = notFoundException,
                    ProblemDetails = new ProblemDetails
                    {
                        Type = notFoundException.GetType().Name,
                        Title = "Resource not found",
                        Detail = notFoundException.Message
                    }
                });
            }
            return false;
        }
    }
}
