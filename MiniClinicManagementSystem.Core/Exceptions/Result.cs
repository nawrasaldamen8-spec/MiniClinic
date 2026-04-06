
using System.Net;

namespace MiniClinicManagementSystem.Core.Exceptions
{

	public class Result
	{
		public bool IsSuccess { get; protected set; }
		public string? Message { get; protected set; }
        public HttpStatusCode? StatusCode { get; protected set; }
		public static Result Success(string? message = null, HttpStatusCode code = HttpStatusCode.OK)
			=> new()
			{
				IsSuccess = true,
				Message = message ?? "Operation completed successfully.",
				StatusCode = code
			};

		public static Result Failure(string message, HttpStatusCode errorCode)
			=> new()
			{
				IsSuccess = false,
				Message = message,
				StatusCode = errorCode
			};
	}

	public class Result<T> : Result
	{
  
        public T? Data { get; set; }

        public static Result<T> Success(T value, string message = "Operation completed successfully.", HttpStatusCode code = HttpStatusCode.OK)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                StatusCode = code,
                Data = value
            };
        }

        public static new Result<T> Failure(string message, HttpStatusCode code = HttpStatusCode.BadRequest) 
            => new()
            {
                  IsSuccess = false,
                  Message = message,
                  StatusCode = code,
				  Data = default
			};

    }
}
