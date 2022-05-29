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
    public class ActiveProductCommand : IRequestHandler<ActiveProductRequest, ActiveProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ActiveProductRequest> _validator;
        public ActiveProductCommand(IUnitOfWork unitOfWork, IValidator<ActiveProductRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public Task<ActiveProductResponse> Handle(ActiveProductRequest request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t=> t.Id == request.CategoryId);
            category.ActiveProduct(request.Id);
            _unitOfWork.Commit();
            return Task.FromResult(new ActiveProductResponse(request.Id));
        }
    }
    public record ActiveProductRequest : IRequest<ActiveProductResponse>
    {
        public long CategoryId { get; set; }
        public long Id { get; set; }
    }
    public record ActiveProductResponse
    {
        private long Id { get; set; }

        public string Message { get; set; }

        public ActiveProductResponse(long id)
        {
            this.Id = id;
            this.Message = "¡Operación Exitosa!";
        }
    }

    public class ActiveProductValidator : AbstractValidator<ActiveProductRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        private Category _category;
        public ActiveProductValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.CategoryId).Must(ExistCategory).WithMessage($"Categoria no encontrada");
            RuleFor(a => new { productRequest = a }).Custom((a, context) =>
            {
                if (ExistCategory(a.productRequest.CategoryId)) {
                    _category.CanActiveProduct(a.productRequest.Id).ToValidationFailure(context);
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
