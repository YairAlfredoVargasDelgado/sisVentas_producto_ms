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
    public class RegisterProductCommand : IRequestHandler<RegisterProductRequest, RegisterProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterProductRequest> _validator;
        public RegisterProductCommand(IUnitOfWork unitOfWork, IValidator<RegisterProductRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public Task<RegisterProductResponse> Handle(RegisterProductRequest request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t=> t.Id == request.CategoryId);
            var product = category.AddProduct(date: request.Date, code: request.Code, name: request.Name, amount: request.Amount, request.Price);
            _unitOfWork.Commit();
            return Task.FromResult(new RegisterProductResponse(product.Id));
        }
    }
    public record RegisterProductRequest : IRequest<RegisterProductResponse>
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
    }
    public record RegisterProductResponse
    {
        private long Id { get; set; }

        public string Message { get; set; }

        public RegisterProductResponse(long id)
        {
            this.Id = id;
            this.Message = "¡Operación Exitosa!";
        }
    }

    public class RegisterProductValidator : AbstractValidator<RegisterProductRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        private Category _category;
        public RegisterProductValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Code).NotEmpty().WithMessage("Nombre no puede ser vacío");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Código no puede ser vacío");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Fecha de registo no puede ser vacía");
            RuleFor(x => x.CategoryId).Must(ExistCategory).WithMessage($"Categoria no encontrada");
            RuleFor(a => new { productRequest = a }).Custom((a, context) =>
            {
                if (ExistCategory(a.productRequest.CategoryId)) {
                    _category.CanAddProduct(a.productRequest.Code,a.productRequest.Amount).ToValidationFailure(context);
                }

            });
        }

        private bool ExistCategory(long id)
        {
            _category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t => t.Id == id && t.Status != "IN", includeProperties: "Products");
            return _category != null;
        }

       
    }
}
