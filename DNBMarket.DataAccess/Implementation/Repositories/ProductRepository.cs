using DNBMarket.BaseLogic.Data.Implementation;
using DNBMarket.DataAccess.Interfaces;
using DNBMarket.Entities.DNBMarket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.DataAccess.Implementation.Repositories
{
    public class ProductRepository : EntityRepositoryBase<Product>,IProductRepository
    {
        public ProductRepository(DbContext context):base(context)
        {

        }
    }
}
