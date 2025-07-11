using Presentation.Enums.ErrorsCode;

namespace Presentation.Exceptions
{
    public class BaseApplicationException : Exception
    {
        public int HttpStatusCode { get; set; }
        public ErrorCode ErrorCode { get; set; } 

        public BaseApplicationException(string message,int httpStatusCode,ErrorCode errorCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
        }

        public BaseApplicationException(string message, int httpStatusCode, ErrorCode errorCode, Exception innerException) : base(message,innerException)
        {
            HttpStatusCode = httpStatusCode;
            ErrorCode = errorCode;
        }

    }
}
