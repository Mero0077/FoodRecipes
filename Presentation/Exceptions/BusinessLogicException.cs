using Presentation.Enums.ErrorsCode;

namespace Presentation.Exceptions
{
    public class BusinessLogicException : BaseApplicationException
    {
        public BusinessLogicException(string message, int httpStatusCode, ErrorCode errorCode) : base(message, StatusCodes.Status400BadRequest, errorCode)
        {
        }

        public BusinessLogicException(string message, int httpStatusCode, ErrorCode errorCode, Exception innerException) : base(message, StatusCodes.Status400BadRequest, errorCode, innerException)
        {
        }
    }
}
