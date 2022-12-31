using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnitofWork.Errors
{
    public class ApiError
    {
        public ApiError(int errorCode, string errorMessage, string errordetails)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Errordetails = errordetails;
        }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string Errordetails { get; set; }

        //to override serialize
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
