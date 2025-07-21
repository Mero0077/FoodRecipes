using Microsoft.AspNetCore.Http;
using Application.Enums.ErrorCodes;

namespace Application.Exceptions
{
    public class UnAuthorizedException : BaseApplicationException
    {
        public UnAuthorizedException(string message, ErrorCodes ErrorCode) : base(message, StatusCodes.Status401Unauthorized, ErrorCode)
        {
        }

        public UnAuthorizedException(string message, ErrorCodes ErrorCode, Exception innerException) : base(message, StatusCodes.Status401Unauthorized, ErrorCode, innerException)
        {
        }
    }
}
