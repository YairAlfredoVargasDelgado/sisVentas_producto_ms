using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SysVentas.Products.Application.Categorys.ModelView;
using SysVentas.Products.Domain.Base;

namespace SysVentas.Products.Application.Categorys.Products
{
    
    public class GetProductForCategoryQuery : IRequestHandler<GetProductForCategoryRequest, GetProductForCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductForCategoryQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public Task<GetProductForCategoryResponse> Handle(GetProductForCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t=> t.Id == request.CategoryId,includeProperties:"Products");
            var productModelView = new ProductModelView();
            var productsModelViws = productModelView.ToListEntity(category.Products.Where(t=> t.Status!="IN").ToList());
            return Task.FromResult(new GetProductForCategoryResponse(category.Id,productsModelViws));
        }
    }
    public class GetProductForCategoryRequest : IRequest<GetProductForCategoryResponse>
    {
        public long CategoryId { get; set; }
    }
    public class GetProductForCategoryResponse
    {
        public long CategoryId { get; set; }
        public List<ProductModelView> Products { get; set; }
        public GetProductForCategoryResponse(long categoryId,List<ProductModelView> categoryModelViews)
        {
            Products = categoryModelViews;
            CategoryId = categoryId;
        }
    }
}
