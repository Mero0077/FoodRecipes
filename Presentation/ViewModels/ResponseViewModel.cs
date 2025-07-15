using Presentation.Enums.ErrorCodes;

namespace Presentation.ViewModels
{
    public class ResponseViewModel<T> 
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorCodes ErrorCodes { get; set; }
        public string? TraceId { get; set; }

        public static ResponseViewModel<T> Success(T data)
        {
            return new ResponseViewModel<T>
            {
                Data = data,
                IsSuccess = true,
                Message = string.Empty,
                ErrorCodes = ErrorCodes.None,
            };
        }

        public static ResponseViewModel<T> Failuer(string message,  string? traceId, ErrorCodes ErrorCode)
        {
            return new ResponseViewModel<T>
            {
                IsSuccess = false,
                Message = message,
                TraceId = traceId,
                ErrorCodes = ErrorCode
            };
        }
    }
}
