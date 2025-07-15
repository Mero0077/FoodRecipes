using Presentation.Enums.ErrorCodes;

namespace Presentation.Exceptions
{
    public class ValidationException : BaseApplicationException
    {
        public ValidationException(string message, int httpStatusCode, ErrorCodes ErrorCode) : base(message, StatusCodes.Status400BadRequest, ErrorCode)
        {
        }

        public ValidationException(string message, int httpStatusCode, ErrorCodes ErrorCode, Exception innerException) : base(message, StatusCodes.Status400BadRequest, ErrorCode, innerException)
        {
        }
    }
}
