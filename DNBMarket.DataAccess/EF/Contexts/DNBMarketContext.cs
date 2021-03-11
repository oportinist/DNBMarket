using System;
using DNBMarket.Entities.DNBMarket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DNBMarket.DataAccess.EF.Contexts
{
    public partial class DNBMarketContext : DbContext
    {
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartProduct> CartProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public DNBMarketContext(DbContextOptions<DNBMarketContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Carts_Users");
            });

            modelBuilder.Entity<CartProduct>(entity =>
            {
                //entity.HasKey(e => new { e.CartId, e.ProductId });

                //entity.HasOne(d => d.Cart)
                //    .WithMany(p => p.CartProducts)
                //    .HasForeignKey(d => d.CartId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_CartProduct_Carts");

                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.CartProducts)
                //    .HasForeignKey(d => d.ProductId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_CartProduct_Products");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}