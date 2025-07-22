using Application.Enums.ErrorCodes;
using Application.Views;

namespace Presentation.ViewModels.ErrorVM
{
    public class SuccessResponseVM<T> : ResponseVM<T>
    {
        public SuccessResponseVM(T Data, string message = "")
        {
            this.Data = Data;
            Message = message;
            IsSuccess = true;
            errorCode = ErrorCodes.None;
        }
    }
}
