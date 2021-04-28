using System;
namespace com.vreshly.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException()
        {
        }

        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}
