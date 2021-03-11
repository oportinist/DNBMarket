using DNBMarket.Common.BaseTypes.Abstract;
using DNBMarket.Entities.DNBMarket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BusinessLogic.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<IList<Product>>> GetAllProduct();
        Task<IDataResult<IList<Product>>> GetAllProductWithPassive();
        Task<int> AddOrUpdateProductAsync(Product model);
        Task<Product> GetProductAsync(int Id);
        Task DeleteAsync(int Id);
    }
}
