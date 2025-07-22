using Application.Enums.ErrorCodes;
using Application.Views;

namespace Presentation.ViewModels.ErrorVM
{
    public class FailureResponseVM<T> : ResponseVM<T>
    {
        public FailureResponseVM(ErrorCodes errorCode, string message = "")
        {
            Message = message;
            IsSuccess = false;
            this.errorCode = errorCode;
        }
    }
}
