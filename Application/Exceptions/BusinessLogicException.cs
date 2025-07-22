using Application.Enums.ErrorCodes;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions
{
    public class BusinessLogicException : BaseApplicationException
    {

        public BusinessLogicException(string message, ErrorCodes ErrorCode) : base(message, StatusCodes.Status403Forbidden, ErrorCode)
        {
        }

        public BusinessLogicException(string message, ErrorCodes ErrorCode, Exception innerException) : base(message, StatusCodes.Status403Forbidden, ErrorCode, innerException)
        {
        }
    }
}
