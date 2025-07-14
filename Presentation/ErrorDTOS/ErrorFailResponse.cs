using Presentation.Enums.ErrorCodes;
using Presentation.ViewModels;

namespace Presentation.ErrorDTOS
{
    public class ErrorFailResponse<T> : ResponseViewModel<T>
    {
        public ErrorFailResponse(string message ,string? traceId,ErrorCodes errorCode)
        {
            Message = message;
            TraceId = traceId;
            ErrorCode = errorCode;
        }
    }
}
