using Presentation.Enums.ErrorCodes;
using Presentation.ErrorDTOS;

namespace Presentation.ViewModels
{
    public class ResponseViewModel<T> 
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public string? TraceId { get; set; }

        public static ResponseViewModel<T> Success(T data)
        {
            return new ResponseViewModel<T>
            {
                Data = data,
                IsSuccess = true,
                Message = string.Empty,
                ErrorCode = ErrorCodes.None,
            };
        }

        public static ResponseViewModel<T> Fail(string message,  string? traceId, ErrorCodes errorCode)
        {
            return new ResponseViewModel<T>
            {
                IsSuccess = false,
                Message = message,
                TraceId = traceId,
                ErrorCode = errorCode
            };
        }
    }
}
