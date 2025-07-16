using Microsoft.AspNetCore.Http;
using Application.Enums.ErrorCodes;

namespace Application.Exceptions
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
