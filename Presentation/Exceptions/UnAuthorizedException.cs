using Presentation.Enums.ErrorsCode;

namespace Presentation.Exceptions
{
    public class UnAuthorizedException : BaseApplicationException
    {
        public UnAuthorizedException(string message, int httpStatusCode, ErrorCode errorCode) : base(message, StatusCodes.Status203NonAuthoritative, errorCode)
        {
        }

        public UnAuthorizedException(string message, int httpStatusCode, ErrorCode errorCode, Exception innerException) : base(message, StatusCodes.Status203NonAuthoritative, errorCode, innerException)
        {
        }
    }
}
