using Presentation.Enums.ErrorsCode;

namespace Presentation.ErrorDTOS
{
    public class ResponseDTO<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string? TraceId { get; set; }
    }
}
