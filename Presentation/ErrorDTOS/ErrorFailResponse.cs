using Presentation.Enums.ErrorsCode;

namespace Presentation.ErrorDTOS
{
    public class ErrorFailResponse<T> : ResponseDTO<T>
    {
        public ErrorFailResponse(string message ,string? traceId,ErrorCode errorCode)
        {
            Message = message;
            TraceId = traceId;
            ErrorCode = errorCode;
        }
    }
}
