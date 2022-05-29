using System;
using System.Collections.Generic;
using SysVentas.Products.Domain.Base;

namespace SysVentas.Products.Domain
{
    public class Category: Entity<long>
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
        public Category(DateTime date, string code, string name )
        {
            Date = date;
            Code = code;
            Name = name;
            Status = StatusView.Get(Base.StatusObject.Active);
            Products = new List<Product>();
        }

        public Product GetProductForCode(string code)
        {
            var product = Products.Find(t => t.Code == code && t.Status != "IN");
            return product;
        }

        public DomainValidation CanInactiveProduct(long id) {
            var validator = new DomainValidation();
            var product = GetProductForId(id);
            if (product == null)
            {
                validator.AddFailed("Inactivar Producto", $"Producto de id {id} no fue encontrado");
            }
            else {
                if (product.Status != StatusView.Get(StatusObject.Active))
                {
                    validator.AddFailed("Activar Producto", $"Producto {Code}-{Name} ya se encuentra Inactivado");
                }
            }
            return validator;
        }

        public void Inactive()
        {
            Status = StatusView.Get(StatusObject.Inactive);
        }

        public void Active()
        {
            Status = StatusView.Get(StatusObject.Active);
        }
        public void InactiveProduct(long idProduct)
        {
            var validator = CanInactiveProduct(idProduct);
            if (!validator.IsValid)
            {
                throw new DomainException(validator);
            }
            var product = GetProductForId(idProduct);
            product.Inactive();

        }

        public DomainValidation CanActiveProduct(long id)
        {
            var validator = new DomainValidation();
            var product = GetProductForId(id);
            if (product == null)
            {
                validator.AddFailed("Activar Producto", $"Producto de id {id} no fue encontrado");
            }
            else {
                if (product.Status != StatusView.Get(StatusObject.Inactive))
                {
                    validator.AddFailed("Activar Producto", $"Producto {Code}-{Name} se encuentra Activado");
                }

            }
            return validator;
        }
        public void ActiveProduct(long idProduct)
        {
            var validator = CanActiveProduct(idProduct);
            if (!validator.IsValid)
            {
                throw new DomainException(validator);
            }
            var product = GetProductForId(idProduct);
            product.Active();

        }

        public DomainValidation CanEditProduct(long id, int amount)
        {
            var validator = new DomainValidation();
            var product = GetProductForId(id);
            if (product == null)
            {
                validator.AddFailed("Editar Producto", $"Producto de id {id} no fue encontrado");
            }
            else {
                if (amount < 0) 
                {
                    validator.AddFailed("Editar Producto", $"Cantidad de producto no puede ser menor que cero");
                }
                
            }
            return validator;

        }
                
        public void EditProduct(long idProduct,string code, string name, int amount)
        {
            var validator = CanEditProduct(idProduct,amount);
            if (!validator.IsValid)
            {
                throw new DomainException(validator);
            }
            var product = GetProductForId(idProduct);
            product.Edit(code, name, amount);
        }

        public DomainValidation CanAddProduct(string code,int amount) {
            var validator = new DomainValidation();
            var product = GetProductForCode(code);
            if (product != null) {
                validator.AddFailed("Agregar Producto",$"Ya se encuentra registrado un producto con el código {code}");
            }
            else
            {
                if (amount < 0)
                {
                    validator.AddFailed("Editar Producto", $"Cantidad de producto no puede ser menor que cero");
                }

            }
            return validator;
        
        }

        public Product AddProduct(DateTime date,string code, string name, int amount) {
            var validator = CanAddProduct(code,amount);
            if (!validator.IsValid) 
            {
                throw new DomainException(validator);
            }

            var product = new Product(date, name, code, amount);
            Products.Add(product);
            return product;
        }
        private Product GetProductForId(long idProduct)
        {
            var product = Products.Find(t => t.Id == idProduct);
            return product;
        }

        public void Editar(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public DomainValidation CanUpdateStockProduct(long idProduct,decimal amount) {
            var validator = new DomainValidation();
            var product = GetProductForId(idProduct);
            if (product == null)
            {
                validator.AddFailed("Actualizar stock a un Producto", $"Producto de id:{idProduct} no fue encontrado en la Categoria {Name}");
            }
            else {
                if (product.Status != StatusView.Get(StatusObject.Active)){
                    validator.AddFailed("Actualizar stock a un Producto", $"Producto [{Code}] [{Name}] se encuentra inactivado");
                }
                if (amount < 0) 
                {
                    var amountTemporal= amount*-1;
                    if (amountTemporal > product.Amount) { 
                    
                        validator.AddFailed("Actualizar stock a un Producto", $"Producto [{product.Code}] [{product.Name}] supera a la cantidad actual en inventario {product.Amount} Cantidad a Reducir {amount*-1}");
                    }
                }
            
            }
            return validator;
        }
        public void UpdateStockProduct(long idProduct, int amount)
        {
            var validator = CanUpdateStockProduct(idProduct, amount);
            if (!validator.IsValid)
            {
                throw new DomainException(validator);
            }
            var product = GetProductForId(idProduct);
            product.UpdateProductStock(amount);

        }
    }

    

    


}
