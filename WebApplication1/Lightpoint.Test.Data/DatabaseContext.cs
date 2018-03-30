using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lightpoint.Test.Data
{
    public class DatabaseContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductsAndStoresEntity>().HasKey(ps => new { ps.ProductId, ps.StoreId });
            modelBuilder.Entity<StoresEntity>().HasIndex(s => s.Name).IsUnique(true);
            modelBuilder.Entity<ProductsEntity>().HasIndex(p => p.Name).IsUnique(true);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ProductsEntity> Products { get; set; }
        public DbSet<StoresEntity> Stores { get; set; }
    }
}
