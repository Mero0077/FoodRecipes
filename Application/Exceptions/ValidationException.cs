using Microsoft.AspNetCore.Http;
using Application.Enums.ErrorCodes;

namespace Application.Exceptions
{
    public class ValidationException : BaseApplicationException
    {
        public ValidationException(string message, ErrorCodes ErrorCode) : base(message, StatusCodes.Status400BadRequest, ErrorCode)
        {
        }

        public ValidationException(string message, ErrorCodes ErrorCode, Exception innerException) : base(message, StatusCodes.Status400BadRequest, ErrorCode, innerException)
        {
        }

    }
}
