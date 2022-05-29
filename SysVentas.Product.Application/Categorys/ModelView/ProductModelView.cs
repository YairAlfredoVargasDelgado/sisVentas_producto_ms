using System;
using SysVentas.Products.Application.Base;
using SysVentas.Products.Domain;

namespace SysVentas.Products.Application.Categorys.ModelView
{
    public class ProductModelView : DTO<long, Product, ProductModelView>
    {
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public long CategoryId { get; set; }
    }
}