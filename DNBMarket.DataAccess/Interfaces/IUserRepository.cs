using DNBMarket.BaseLogic.Data.Interfaces;
using DNBMarket.Entities.DNBMarket;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.DataAccess.Interfaces
{
    public interface IUserRepository : IEntityRepositoryBase<User>
    {
    }
}
