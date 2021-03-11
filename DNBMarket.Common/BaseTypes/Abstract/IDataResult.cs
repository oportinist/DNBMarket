using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.Common.BaseTypes.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        public T Data { get; }
    }
}
