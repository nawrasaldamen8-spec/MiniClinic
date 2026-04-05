using System.Net;

namespace MiniClinicManagementSystem.Core.Exceptions
{
    public class AppException(HttpStatusCode statusCode,  string message) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}
