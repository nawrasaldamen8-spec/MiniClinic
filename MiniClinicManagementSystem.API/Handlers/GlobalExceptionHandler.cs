
using Microsoft.AspNetCore.Diagnostics;
using MiniClinicManagementSystem.Core.Exceptions;
using System.Net;

namespace MiniClinicManagementSystem.API.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var statusCode = exception is AppException apiException ? apiException.StatusCode : HttpStatusCode.InternalServerError;

            httpContext.Response.StatusCode = (int)statusCode;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                Code = (int)statusCode,
                Status = statusCode.ToString(),
                Message = exception.Message
            },
            cancellationToken);

            return true;
        }
    }
}

