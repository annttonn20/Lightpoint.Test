using System;
using Lightpoint.Test.Business.Interface;
using Lightpoint.Test.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Lightpoint.Test.Business
{
    public static class BusinessFacade
    {
        public static void Initialize (IServiceCollection service)
        {
            DataFacade.Configuration = Configuration;
            DataFacade.Initialize(service);
            service.AddTransient<IManager, Manager>(m => new Manager(m.GetService<DatabaseContext>()));
            service.AddTransient<IProductManager, ProductManager>(m => new ProductManager(m.GetService<DatabaseContext>()));
            service.AddTransient<IStoreManager, StoreManager>(m => new StoreManager(m.GetService<DatabaseContext>()));
        }

        public static IConfiguration Configuration { get; set; }
    }
}
