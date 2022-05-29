using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysVentas.Products.Application.Base;
using SysVentas.Products.Domain;

namespace SysVentas.Products.Application.Categorys.ModelView
{
    public class CategoryModelView: DTO<long,Category,CategoryModelView>
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<ProductModelView> Products { get; set; }
    }

}
