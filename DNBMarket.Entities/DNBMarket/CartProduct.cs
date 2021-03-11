using DNBMarket.BaseLogic.Data.Interfaces;
using DNBMarket.BaseLogic.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace DNBMarket.Entities.DNBMarket
{
    public partial class CartProduct : EntityBase, IEntity
    {
        public CartProduct()
        {
            Cart = new Cart();
            Product = new Product();
        }
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int? PaymentType { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsPaid { get; set; } 

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}