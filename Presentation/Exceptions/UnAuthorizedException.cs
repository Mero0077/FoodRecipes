using Presentation.Enums.ErrorCodes;

namespace Presentation.Exceptions
{
    public class UnAuthorizedException : BaseApplicationException
    {
        public UnAuthorizedException(string message, int httpStatusCode, ErrorCodes ErrorCode) : base(message, StatusCodes.Status203NonAuthoritative, ErrorCode)
        {
        }

        public UnAuthorizedException(string message, int httpStatusCode, ErrorCodes ErrorCode, Exception innerException) : base(message, StatusCodes.Status203NonAuthoritative, ErrorCode, innerException)
        {
        }
    }
}
