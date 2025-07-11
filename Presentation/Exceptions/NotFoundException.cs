using Presentation.Enums.ErrorsCode;

namespace Presentation.Exceptions
{
    public class NotFoundException : BaseApplicationException
    {
        public NotFoundException(string message, int httpStatusCode, ErrorCode errorCode) : base(message, StatusCodes.Status400BadRequest, errorCode)
        {
        }
        public NotFoundException(string message, int httpStatusCode, ErrorCode errorCode, Exception innerException) : base(message, StatusCodes.Status400BadRequest, errorCode, innerException)
        {
        }
    }
}
