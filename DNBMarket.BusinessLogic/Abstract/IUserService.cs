﻿using DNBMarket.Common.BaseTypes.Abstract;
using DNBMarket.Entities.DNBMarket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BusinessLogic.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<User>> GetUser(string email, string password);
    }
}
