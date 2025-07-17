using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Views
{
    public record RequestResult<T>(T Data, bool IsSuccess, string Message, ErrorCode ErrorCode)
    {

        public static RequestResult<T> Success(T data, string message = "")
        {
            return new RequestResult<T>(data, true, message, ErrorCode.None);
        }


        public static RequestResult<T> Failure(ErrorCode errorCode, string message = "")
        {
            return new RequestResult<T>(default, false, message, errorCode);
        }
    }
}
