using DNBMarket.Common.BaseTypes.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.Common.BaseTypes.Implementation
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = message;
        }
        public DataResult(ResultStatus resultStatus, string message, T data, Exception exceptionMessage)
        {
            ResultStatus = resultStatus;
            Data = data;
            Message = message;
            ExceptionMessage = exceptionMessage;
        }
        public T Data { get; }

        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public Exception ExceptionMessage { get; }
    }
}
