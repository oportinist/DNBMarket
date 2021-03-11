using DNBMarket.BaseLogic.Data.Interfaces;
using DNBMarket.BaseLogic.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace DNBMarket.Entities.DNBMarket
{
    public partial class User : EntityBase, IEntity
    {
        public User()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 

        public virtual ICollection<Cart> Carts { get; set; }
    }
}