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

namespace SysVentas.Products.Application.Categorys
{
    public class RegisterCategoryCommand : IRequestHandler<RegisterCategoryRequest, RegisterCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterCategoryRequest> _validator;
        public RegisterCategoryCommand(IUnitOfWork unitOfWork, IValidator<RegisterCategoryRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public Task<RegisterCategoryResponse> Handle(RegisterCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new Category(date:request.Date,code:request.Code,name:request.Name);
            _unitOfWork.CategorysRepository.Add(category);
            _unitOfWork.Commit();
            return Task.FromResult(new RegisterCategoryResponse(category.Id));
        }
    }
    public record RegisterCategoryRequest : IRequest<RegisterCategoryResponse>{
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }

    }
    public record RegisterCategoryResponse
    {
        private long id { get; set; }

        public string Message { get; set; }

        public RegisterCategoryResponse(long id)
        {
            this.id = id;
            this.Message = "¡Operación Exitosa!";
        }
    }

    public class RegisterCategoryValidator : AbstractValidator<RegisterCategoryRequest> {
        private readonly IUnitOfWork _unitOfWork;        
        public RegisterCategoryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Code).NotEmpty().WithMessage("Nombre no puede ser vacío");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Código no puede ser vacío");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Fecha de registo no puede ser vacía");
            RuleFor(x => x.Code).Must(ExistCategory).WithMessage($"El código de esta Categoría ya se encuentra registrado");
        }

        private bool ExistCategory(string code)
        {
            var category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t=> t.Code == code);
            return category == null;
        }
    }
}
