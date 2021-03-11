using DNBMarket.DataAccess.EF.Contexts;
using DNBMarket.DataAccess.Implementation.Repositories;
using DNBMarket.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DNBMarketContext _ctx;
        private ProductRepository productRepo;
        private CartRepository cartRepo;
        private UserRepository userRepo;
        private CartProductRepository cartProductRepo;

        public UnitOfWork(DNBMarketContext ctx)
        {
            _ctx = ctx;
        }

        public IProductRepository ProdcutRepo => productRepo ?? new ProductRepository(_ctx);

        public ICartRepository CartRepo => cartRepo ?? new CartRepository(_ctx);

        public IUserRepository UserRepo => userRepo ?? new UserRepository(_ctx);

        public ICartProductRepository CartProductRepo => cartProductRepo ?? new CartProductRepository(_ctx);

        public async ValueTask DisposeAsync()
        {
            await _ctx.DisposeAsync();
        }

        public int SaveChanges()
        {
            ProcessEntityControl();
            if (!_ctx.ChangeTracker.Entries().Any(x => x.State == EntityState.Modified || x.State == EntityState.Deleted || x.State == EntityState.Added))
                return 1;
            return _ctx.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            ProcessEntityControl();
            if (!_ctx.ChangeTracker.Entries().Any(p => p.State == EntityState.Modified || p.State == EntityState.Deleted || p.State == EntityState.Added))
                return 1;
            return await _ctx.SaveChangesAsync();
        } 
        private void ProcessEntityControl()
        {
            var auditEntities = _ctx.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).Select(x => new { x.Entity, x.State });
            if (auditEntities != null)
            {
                foreach (var obj in auditEntities)
                {
                    var auditEntity = obj.Entity;
                    var properties = auditEntities.GetType()
                                                  .GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                                                  .Where(x => x.CanWrite).ToDictionary(x => x.Name, x => x);
                    if (obj.State == EntityState.Added || obj.State == EntityState.Detached)
                    {
                        if (properties.ContainsKey("CreateDate"))
                            properties["CreateDate"].SetValue(auditEntity, DateTime.Now);
                    }
                    if (properties.ContainsKey("UpdatedDate"))
                        properties["UpdateDate"].SetValue(auditEntity, DateTime.Now);
                }
            }
        }
    }
}
