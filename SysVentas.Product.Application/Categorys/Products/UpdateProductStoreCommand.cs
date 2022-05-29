using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SysVentas.Products.Domain;
using SysVentas.Products.Domain.Base;
using SysVentas.Products.Application.Base;


namespace SysVentas.Products.Application.Categorys.Products
{
    public class UpdateProductStoreCommand : IRequestHandler<UpdateProductStoreRequest, UpdateProductStoreResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProductStoreRequest> _validator;

        public UpdateProductStoreCommand(IUnitOfWork unitOfWork, IValidator<UpdateProductStoreRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public Task<UpdateProductStoreResponse> Handle(UpdateProductStoreRequest request, CancellationToken cancellationToken)
        {
            var productsRequest = request.Products;
            var products = ObtenerProduct(productsRequest);
            productsRequest.ForEach(productRequest => {
                products.ForEach(p => {
                    if (productRequest.ProductId == p.Id) {
                        p.UpdateProductStock(productRequest.Cuantity);
                    };
                });
            });
            _unitOfWork.Commit();
            return Task.FromResult(new UpdateProductStoreResponse());
        }

        private List<Product> ObtenerProduct(List<ProductStoreModelView> productStoreModelViews) {
            var products = new List<Product>();
            productStoreModelViews.ForEach(p =>
            {
                var product = _unitOfWork.CategorysRepository.GetProduct(p.ProductId);
                if (product != null)
                {
                    products.Add(product);
                }
            });

            return products;
        }
    }

    public class UpdateProductStoreRequest : IRequest<UpdateProductStoreResponse>
    {
        public List<ProductStoreModelView> Products { get; set; }
    }
    public class UpdateProductStoreResponse
    {
        public string Message { get; set; }

        public UpdateProductStoreResponse()
        {
            this.Message = "¡Operación Exitosa!";
        }
    }

    public class ProductStoreValidator : AbstractValidator<UpdateProductStoreRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductsValidator ProductValidator { get; set; }
        public ProductStoreValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            ProductValidator = new ProductsValidator(this._unitOfWork);
            RuleForEach(x => x.Products).SetValidator(ProductValidator).When(x=> x.Products.Count>0);
        }
    
    }

    public class ProductsValidator : AbstractValidator<ProductStoreModelView>
    {
        private readonly IUnitOfWork _unitOfWork;
        public Product Product { get; set; }
        public ProductsValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Product = new Product();
            RuleFor(x => x.ProductId).Must(ExistirProduct).WithMessage(t=> $"Producto de id {t.ProductId} no encontrado");
            RuleFor(a => new { product = a }).Custom((a, context) =>
            {
                if (ExistirProduct(a.product.ProductId))
                {
                    Product.Category.CanUpdateStockProduct(a.product.ProductId,a.product.Cuantity).ToValidationFailure(context);
                }
            });
        }

        public bool ExistirProduct(long productId)
        {
            Product = _unitOfWork.CategorysRepository.GetProduct(productId);
            if (Product != null)
            {
                return true;
            }
            return false;
        }
    }

    public class ProductStoreModelView
    {
        public long ProductId { get; set; }
        public decimal Cuantity { get; set; }

    }
}
