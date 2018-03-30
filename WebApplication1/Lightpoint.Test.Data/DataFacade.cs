using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightpoint.Test.Data
{
    public static class DataFacade
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();
            DatabaseContext.ConnectionString = (Configuration.GetConnectionString("Database"));
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Database.Migrate();
            }
        }

        public static IConfiguration Configuration { get; set; }
    }
}
