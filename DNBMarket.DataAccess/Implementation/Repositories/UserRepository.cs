using DNBMarket.BaseLogic.Data.Implementation;
using DNBMarket.DataAccess.Interfaces;
using DNBMarket.Entities.DNBMarket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.DataAccess.Implementation.Repositories
{
    public class UserRepository : EntityRepositoryBase<User>,IUserRepository
    {
        public UserRepository(DbContext context):base(context)
        {

        }
    }
}
