
using Application.Enums.ErrorCodes;

namespace Application.Exceptions
{
    public class BaseApplicationException : Exception
    {
        public int HttpStatusCode { get; set; }
        public ErrorCodes ErrorCodes { get; set; } 

        public BaseApplicationException(string message,int httpStatusCode, ErrorCodes ErrorCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorCodes = ErrorCode;
        }

        public BaseApplicationException(string message, int httpStatusCode, ErrorCodes ErrorCode, Exception innerException) : base(message,innerException)
        {
            HttpStatusCode = httpStatusCode;
            ErrorCodes = ErrorCode;
        }

    }
}
