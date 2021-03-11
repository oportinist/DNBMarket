using DNBMarket.Common.BaseTypes.Abstract;
using DNBMarket.Entities.DNBMarket;
using DNBMarket.Models.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BusinessLogic.Abstract
{
    public interface ICartService
    {
        Task<IDataResult<CartModel>> GetCurrentCartAsync();
    }
}
