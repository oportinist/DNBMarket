using DNBMarket.BaseLogic.Data.Implementation;
using DNBMarket.DataAccess.Interfaces;
using DNBMarket.Entities.DNBMarket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.DataAccess.Implementation.Repositories
{
    public class CartRepository : EntityRepositoryBase<Cart>,ICartRepository
    {
        public CartRepository(DbContext context):base(context)
        {

        }
    }
}
