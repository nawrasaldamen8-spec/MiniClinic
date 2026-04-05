
using Microsoft.AspNetCore.Diagnostics;
using MiniClinicManagementSystem.Core.Exceptions;
using System.Net;

namespace MiniClinicManagementSystem.API.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string message;

            if (exception is AppException appEx)
            {
                statusCode = appEx.StatusCode;
                message = appEx.Message;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "Something went wrong"; 
            }

            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";

            var result = new Result<object>
            {
                IsSuccess = false,
                Message = message,
                Code = statusCode
            };

            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}

