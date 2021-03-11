using System;
using System.Collections.Generic;
using System.Text;
using DNBMarket.Entities.DNBMarket;

namespace DNBMarket.Models.Cart
{
    public class CartModel
    {
        public CartModel()
        {
            CartProducts = new List<CartProduct>();
        }
        public DNBMarket.Entities.DNBMarket.Cart Cart { get; set; }
        public List<CartProduct> CartProducts { get; set; }
    }
}
