using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVentas.Products.Domain;
using SysVentas.Products.Infrastructure.Data.Base;
using SysVentas.Products.Infrastructure.Data.Configurations;

namespace SysVentas.Products.Infrastructure.Data
{
    public class ProductDataContext: DbContextBase
    {
        public ProductDataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Domain.Product> Products { get; set; }
        public static string DefaultSchema => "Productos";
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }
    }
}
