using DNBMarket.BaseLogic.Data.Interfaces;
using DNBMarket.BaseLogic.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace DNBMarket.Entities.DNBMarket
{
    public partial class Cart : EntityBase, IEntity
    {
        public Cart()
        {
            CartProducts = new HashSet<CartProduct>();
        }

        public int Id { get; set; }
        public string CartKey { get; set; }
        public int? UserId { get; set; } 

        public virtual User User { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}