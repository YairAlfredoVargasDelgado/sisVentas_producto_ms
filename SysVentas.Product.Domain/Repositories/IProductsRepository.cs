using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVentas.Products.Domain.Base;

namespace SysVentas.Products.Domain.Repositories
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
    }
}
