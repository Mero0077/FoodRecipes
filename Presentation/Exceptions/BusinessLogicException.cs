using Presentation.Enums.ErrorCodes;

namespace Presentation.Exceptions
{
    public class BusinessLogicException : BaseApplicationException
    {
        public BusinessLogicException(string message, int httpStatusCode, ErrorCodes ErrorCode) : base(message, StatusCodes.Status400BadRequest, ErrorCode)
        {
        }

        public BusinessLogicException(string message, int httpStatusCode, ErrorCodes ErrorCode, Exception innerException) : base(message, StatusCodes.Status400BadRequest, ErrorCode, innerException)
        {
        }
    }
}
