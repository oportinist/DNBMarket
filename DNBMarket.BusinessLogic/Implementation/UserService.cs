using DNBMarket.BusinessLogic.Abstract;
using DNBMarket.Common.BaseTypes.Abstract;
using DNBMarket.Common.BaseTypes.Implementation;
using DNBMarket.DataAccess.Interfaces;
using DNBMarket.Entities.DNBMarket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BusinessLogic.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<User>> GetUser(string email, string password)
        {
            try
            {
                var data = await _unitOfWork.UserRepo.FirtsOrDefaultAsync(x => x.Email == email && x.Password == password);
                if (data != null)
                {
                    return new DataResult<User>(Common.ResultStatus.Success, data);
                }
                else
                {
                    return new DataResult<User>(Common.ResultStatus.Error, "Kullanıcı Bulunamadı", null);
                }
            }
            catch (Exception ex)
            {
                return new DataResult<User>(Common.ResultStatus.Error, "hata", null,new Exception(ex.Message));
            }
        }
    }
}
