using Presentation.Enums.ErrorsCode;

namespace Presentation.Exceptions
{
    public class ValidationException : BaseApplicationException
    {
        public ValidationException(string message, int httpStatusCode, ErrorCode errorCode) : base(message, StatusCodes.Status400BadRequest, errorCode)
        {
        }

        public ValidationException(string message, int httpStatusCode, ErrorCode errorCode, Exception innerException) : base(message, StatusCodes.Status400BadRequest, errorCode, innerException)
        {
        }
    }
}
