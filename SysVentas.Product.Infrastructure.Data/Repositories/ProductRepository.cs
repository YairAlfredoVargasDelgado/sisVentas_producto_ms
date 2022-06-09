using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVentas.Products.Domain;
using SysVentas.Products.Domain.Repositories;
using SysVentas.Products.Infrastructure.Data.Base;
using SysVentas.Products.Infrastructure.Data.Repositories;

namespace SysVentas.Products.Infrastructure.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductRepository(IDbContext context) : base(context)
        {
        }
    }
}
