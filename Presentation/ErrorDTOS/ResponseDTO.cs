using Presentation.Enums.ErrorCodes;

namespace Presentation.ErrorDTOS
{

    [Obsolete("This class is deprecated. Use ResponseViewModel<T> instead.")]
    public class ResponseDTO<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public string? TraceId { get; set; }
    }

}
