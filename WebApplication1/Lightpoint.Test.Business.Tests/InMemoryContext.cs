using Lightpoint.Test.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightpoint.Test.Business.Tests
{
    internal class InMemoryContext : DatabaseContext
    {
        private readonly string dbname;

        public InMemoryContext(string dbname)
        {
            this.dbname = dbname;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(dbname);
        }
    }
}
