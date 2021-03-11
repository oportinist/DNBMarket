using DNBMarket.BusinessLogic.Abstract;
using DNBMarket.Common.BaseTypes.Abstract;
using DNBMarket.Common.BaseTypes.Implementation;
using DNBMarket.Common.EntityEnums;
using DNBMarket.Common.Static;
using DNBMarket.DataAccess.Interfaces;
using DNBMarket.Entities.DNBMarket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DNBMarket.BusinessLogic.Implementation
{
    public class CartProductService : ICartProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> AddToCartAsync(int productId)
        {
            try
            {
                var cartData = await GetCureentCartProduct();
                if (cartData != null)
                {
                    var cartProductModel = new CartProduct();
                    cartProductModel.CartId = cartData.Id;
                    cartProductModel.ProductId = productId;
                    cartProductModel.IsPaid = false;
                    await _unitOfWork.CartProductRepo.AddAsync(cartProductModel);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    var cartModel = new Cart();
                    cartModel.CartKey = Guid.NewGuid().ToString();
                    cartModel.UserId = 1;
                    var cartEntity = await _unitOfWork.CartRepo.AddAsync(cartModel);
                    await _unitOfWork.SaveChangesAsync();
                    var cartProductModel = new CartProduct();
                    cartProductModel.CartId = cartEntity.Id;
                    cartProductModel.ProductId = productId;
                    cartProductModel.IsPaid = false;
                    await _unitOfWork.CartProductRepo.AddAsync(cartProductModel);
                    await _unitOfWork.SaveChangesAsync();
                }
                return "Sepete Eklendi";
            }
            catch (Exception ex)
            {
                return $"Sepete Eklemede Hata : {ex.Message}";
            }
        }

        public async Task<IDataResult<string>> PayAsync(int cartId)
        {
            try
            {
                var cartProducts = await _unitOfWork.CartProductRepo.WhereAsync(x => x.CartId == cartId);
                var cart = (await _unitOfWork.CartRepo.WhereAsync(x => x.Id == cartId)).FirstOrDefault();
                cart.Status = (int)EntityStatus.Passive;
                foreach (var item in cartProducts)
                {
                    item.IsPaid = true;
                    item.Status = (int)EntityStatus.Passive;
                }
                await _unitOfWork.SaveChangesAsync();
                return new DataResult<string>(Common.ResultStatus.Success,CommonConst.SuccessPay,null); 
            }
            catch (Exception ex)
            {
                return new DataResult<string>(Common.ResultStatus.Error, CommonConst.ErrorMessage, null, new Exception(ex.Message));
            }
        }

        private async Task<Cart> GetCureentCartProduct()
        {
            var data = (await _unitOfWork.CartRepo.WhereAsync(x => x.Status == 1))?.OrderByDescending(x => x.Id)?.FirstOrDefault();
            return data;
        }
    }
}
