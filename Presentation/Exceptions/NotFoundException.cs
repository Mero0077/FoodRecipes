using Presentation.Enums.ErrorCodes;

namespace Presentation.Exceptions
{
    public class NotFoundException : BaseApplicationException
    {
        public NotFoundException(string message, int httpStatusCode, ErrorCodes ErrorCode) : base(message, StatusCodes.Status400BadRequest, ErrorCode)
        {
        }
        public NotFoundException(string message, int httpStatusCode, ErrorCodes ErrorCode, Exception innerException) : base(message, StatusCodes.Status400BadRequest, ErrorCode, innerException)
        {
        }
    }
}
