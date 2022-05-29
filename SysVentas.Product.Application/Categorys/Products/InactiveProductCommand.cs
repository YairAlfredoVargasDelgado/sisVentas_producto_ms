using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using SysVentas.Products.Application.Base;
using SysVentas.Products.Domain;
using SysVentas.Products.Domain.Base;

namespace SysVentas.Products.Application.Categorys.Products
{
    public class InactiveProductCommand : IRequestHandler<InactiveProductRequest, InactiveProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<InactiveProductRequest> _validator;
        public InactiveProductCommand(IUnitOfWork unitOfWork, IValidator<InactiveProductRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public Task<InactiveProductResponse> Handle(InactiveProductRequest request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t=> t.Id == request.CategoryId);
            category.InactiveProduct(request.Id);
            _unitOfWork.Commit();
            return Task.FromResult(new InactiveProductResponse(request.Id));
        }
    }
    public record InactiveProductRequest : IRequest<InactiveProductResponse>
    {
        public long CategoryId { get; set; }
        public long Id { get; set; }
    }
    public record InactiveProductResponse
    {
        private long Id { get; set; }

        public string Message { get; set; }

        public InactiveProductResponse(long id)
        {
            this.Id = id;
            this.Message = "¡Operación Exitosa!";
        }
    }

    public class InactiveProductValidator : AbstractValidator<InactiveProductRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        private Category _category;
        public InactiveProductValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.CategoryId).Must(ExistCategory).WithMessage($"Categoria no encontrada");
            RuleFor(a => new { productRequest = a }).Custom((a, context) =>
            {
                if (ExistCategory(a.productRequest.CategoryId)) {
                    _category.CanInactiveProduct(a.productRequest.Id).ToValidationFailure(context);
                }

            });
        }

        private bool ExistCategory(long id)
        {
            _category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t => t.Id == id, includeProperties: "Products");
            return _category != null;
        }

       
    }
}
