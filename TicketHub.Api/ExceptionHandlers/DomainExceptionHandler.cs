using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketHub.Domain.Exceptions;

namespace TicketHub.Api.ExceptionHandlers
{
    internal sealed class DomainExceptionHandler(IProblemDetailsService problemDetailsService,
                                    ILogger<DomainExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is DomainException domainException)
            {
                logger.LogWarning("Domain rule violation: {Code} {Message}", domainException.Code, domainException.Message);
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    Exception = domainException,
                    ProblemDetails = new ProblemDetails
                    {
                        Type = domainException.GetType().Name,
                        Title = "Domain error",
                        Detail = domainException.Message,
                        Extensions =
                        {
                            ["code"] = domainException.Code
                        }
                    }
                });
            }
            return false;
        }
    }
}
