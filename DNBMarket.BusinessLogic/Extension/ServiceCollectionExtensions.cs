using DNBMarket.BusinessLogic.Abstract;
using DNBMarket.BusinessLogic.Implementation;
using DNBMarket.DataAccess.Implementation;
using DNBMarket.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.BusinessLogic.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<ICartService, CartService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ICartProductService, CartProductService>();
            return serviceCollection;
        }
    }
}
