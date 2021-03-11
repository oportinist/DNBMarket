using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.DataAccess.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IProductRepository ProdcutRepo { get; }
        ICartRepository CartRepo { get; }
        IUserRepository UserRepo { get; }
        ICartProductRepository CartProductRepo { get; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
