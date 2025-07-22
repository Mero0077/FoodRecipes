using Application.Enums.ErrorCodes;
using Application.Views;

namespace Presentation.ViewModels.ErrorVM
{
    public class ResponseVM<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ErrorCodes errorCode { get; set; }
    }
}
