using Application.Views;

namespace Presentation.ViewModels
{
    public record EndpointResponse<T>(T Data, bool IsSuccess, string Message, ErrorCode ErrorCode)
    {
        public static EndpointResponse<T> Success(T data, string message = "")
        {
            return new EndpointResponse<T>(data, true, message, ErrorCode.None);
        }

        public static EndpointResponse<T> Failure(ErrorCode errorCode, string message)
        {
            return new EndpointResponse<T>(default, false, message, errorCode);
        }

    }
}
