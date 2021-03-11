using DNBMarket.BusinessLogic.Abstract;
using DNBMarket.Common.BaseTypes.Abstract;
using DNBMarket.Common.BaseTypes.Implementation;
using DNBMarket.DataAccess.Interfaces;
using DNBMarket.Entities.DNBMarket;
using DNBMarket.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BusinessLogic.Implementation
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<CartModel>> GetCurrentCartAsync()
        {
            var model = new CartModel();
            var currentCart = (await _unitOfWork.CartRepo.WhereAsync(x => x.Status == 1, y => y.CartProducts))?.OrderByDescending(x => x.Id)?.FirstOrDefault() ?? null;
            if (currentCart != null)
            {
                var cartProducts = await _unitOfWork.CartProductRepo.WhereAsync(x => x.CartId == currentCart.Id,x => x.Product);
                var productIds = cartProducts.Select(x => x.ProductId);
                var products = (await _unitOfWork.ProdcutRepo.WhereAsync(x => productIds.Contains(x.Id))).ToList(); 
                foreach (var item in cartProducts)
                {
                    item.Product = products.Where(x => x.Id == item.ProductId).FirstOrDefault();
                }
                model.Cart = currentCart;
                model.CartProducts = cartProducts;
                return new DataResult<CartModel>(Common.ResultStatus.Success, model);
            }
            else
            {
                return new DataResult<CartModel>(Common.ResultStatus.Warning, "Sepette Ürün Bulunmamaktadır", null);
            }
        }
    }
}
