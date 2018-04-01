using System;
using Lightpoint.Test.Business.Interface;
using Lightpoint.Test.Business.Structure;
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
            service.AddTransient<IManager<ProductStruct>, ProductManager>(m => new ProductManager(m.GetService<DatabaseContext>()));
            service.AddTransient<IManager<StoreStruct>, StoreManager>(m => new StoreManager(m.GetService<DatabaseContext>()));
        }

        public static IConfiguration Configuration { get; set; }
    }
}
