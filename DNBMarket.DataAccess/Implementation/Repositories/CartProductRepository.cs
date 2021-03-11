using DNBMarket.BaseLogic.Data.Implementation;
using DNBMarket.DataAccess.Interfaces;
using DNBMarket.Entities.DNBMarket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.DataAccess.Implementation.Repositories
{
    public class CartProductRepository : EntityRepositoryBase<CartProduct>,ICartProductRepository
    {
        public CartProductRepository(DbContext context):base(context)
        {

        }
    }
}
