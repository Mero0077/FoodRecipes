using Microsoft.AspNetCore.Http;
using Application.Enums.ErrorCodes;

namespace Application.Exceptions
{
    public class NotFoundException : BaseApplicationException
    {
        public NotFoundException(string message, ErrorCodes ErrorCode) : base(message, StatusCodes.Status404NotFound, ErrorCode)
        {
        }
        public NotFoundException(string message, ErrorCodes ErrorCode, Exception innerException) : base(message, StatusCodes.Status404NotFound, ErrorCode, innerException)
        {
        }
    }
}
