using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SysVentas.Products.Domain;
using SysVentas.Products.Domain.Base;

namespace SysVentas.Products.Application.Categorys.Products
{
    public class GetProductByIdHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public GetProductByIdHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Product> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unitOfWork.ProductsRepository.Find(request.ProductId));
        }
    }

    public class GetProductByIdRequest : IRequest<IEnumerable<Product>>
    {
        public GetProductByIdRequest(long productId)
        {
            ProductId = productId;
        }

        public long ProductId { get; set; }
    }
}
