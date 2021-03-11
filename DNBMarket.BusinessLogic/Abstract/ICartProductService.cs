using DNBMarket.Common.BaseTypes.Abstract;
using DNBMarket.Entities.DNBMarket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BusinessLogic.Abstract
{
    public interface ICartProductService
    { 
        Task<string> AddToCartAsync(int productId);
        Task<IDataResult<string>> PayAsync(int cartId);
    }
}
