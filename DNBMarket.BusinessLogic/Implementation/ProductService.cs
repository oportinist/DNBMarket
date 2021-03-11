using DNBMarket.BusinessLogic.Abstract;
using DNBMarket.Common.BaseTypes.Abstract;
using DNBMarket.Common.BaseTypes.Implementation;
using DNBMarket.Common.EntityEnums;
using DNBMarket.Common.Static;
using DNBMarket.DataAccess.Interfaces;
using DNBMarket.Entities.DNBMarket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BusinessLogic.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddOrUpdateProductAsync(Product model)
        {
            #region Fotoğraf İşlemleri

            //string imageExtension = Path.GetExtension(model.CoverImage);
            //string imageName = Guid.NewGuid() + imageExtension;
            //string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/dist/img/product/{imageName}");
            //using var stream = new FileStream(path, FileMode.Create);  

            #endregion

            if (model.Id > 0)
            {
                var data = await _unitOfWork.ProdcutRepo.FirtsOrDefaultAsync(x => x.Id == model.Id);
                if (data !=  null)
                {
                    data.Name = model.Name;
                    data.UnitPrice = model.UnitPrice;
                    data.UnitsInStock = model.UnitsInStock;
                    data.CoverImage = CommonConst.ProductImage;
                    await _unitOfWork.SaveChangesAsync();
                }
                return data.Id;
            }
            else
            {
                model.CoverImage = CommonConst.ProductImage;
                await _unitOfWork.ProdcutRepo.AddAsync(model);
                return await _unitOfWork.SaveChangesAsync();
            } 
        }

        public async Task DeleteAsync(int Id)
        {
            var data = await _unitOfWork.ProdcutRepo.FirtsOrDefaultAsync(x => x.Id == Id);
            data.Status = (int)EntityStatus.Deleted;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IDataResult<IList<Product>>> GetAllProduct()
        {
            try
            {
                var data = await _unitOfWork.ProdcutRepo.ToListAsync();
                if (data != null && data.Count > 0)
                {
                    return new DataResult<IList<Product>>(Common.ResultStatus.Success, data);
                }
                else
                {
                    return new DataResult<IList<Product>>(Common.ResultStatus.Info, "Data Yok", null);
                }
            }
            catch (Exception ex)
            {
                return new DataResult<IList<Product>>(Common.ResultStatus.Error, "Hata", null, new Exception(ex.Message));
            }
        }

        public Task<IDataResult<IList<Product>>> GetAllProductWithPassive()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductAsync(int Id)
        {
            return  await _unitOfWork.ProdcutRepo.FirtsOrDefaultAsync(x => x.Id == Id);
        }
    }
}
