using DNBMarket.BaseLogic.Data.Interfaces;
using DNBMarket.BaseLogic.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace DNBMarket.Entities.DNBMarket
{
    public partial class Product : EntityBase, IEntity
    {
        public Product()
        {
            CartProducts = new HashSet<CartProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public string CoverImage { get; set; } 

        public virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}