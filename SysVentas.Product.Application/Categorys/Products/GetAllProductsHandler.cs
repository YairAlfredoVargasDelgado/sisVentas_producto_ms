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
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, IEnumerable<Product>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllProductsHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Product>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(unitOfWork.ProductsRepository.GetAll());
        }
    }

    public class GetAllProductsRequest : IRequest<IEnumerable<Product>>
    {
    }
}
