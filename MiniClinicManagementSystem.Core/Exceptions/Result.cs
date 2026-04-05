
using System.Net;

namespace MiniClinicManagementSystem.Core.Exceptions
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public  HttpStatusCode Code { get; set; }
        public T? Value { get; set; }

        public static Result<T> Success(T value, string message = "Operation completed successfully.", HttpStatusCode code = HttpStatusCode.OK)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                Code = code,
                Value = value
            };
        }

        public static Result<T> Failure(string message, HttpStatusCode code = HttpStatusCode.BadRequest) 
            => new()
            {
                  IsSuccess = false,
                  Message = message,
                  Code = code
              };

    }
}
