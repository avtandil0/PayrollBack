using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollServer.Models
{
    public class Result
    {
        public const string SUCCESS_MESSAGE = "OK";
        public const int SUCCESS_CODE = 0;

        public Result(bool isSuccess, int code, string message)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
        }

        public Result(bool isSuccess, int code, string message, string id)
        {
            IsSuccess = isSuccess;
            Code = code;
            Message = message;
            Id = id;
        }

        public int Code { get; protected set; }
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }
        public string Id { get; protected set; }

        static private readonly Result successesult = new Result(true, SUCCESS_CODE, SUCCESS_MESSAGE);

        static public Result SuccessInstance()
        {
            return successesult;
        }
    }
}
