using Application.Enums.ErrorCodes;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions
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
