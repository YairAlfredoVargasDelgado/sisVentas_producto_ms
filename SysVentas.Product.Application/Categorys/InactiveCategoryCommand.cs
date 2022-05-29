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
    public class InactiveCategoryCommand : IRequestHandler<InactiveCategoryRequest, InactiveCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<InactiveCategoryRequest> _validator;
        public InactiveCategoryCommand(IUnitOfWork unitOfWork, IValidator<InactiveCategoryRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public Task<InactiveCategoryResponse> Handle(InactiveCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t => t.Id == request.Id);
            category.Inactive();
            _unitOfWork.Commit();
            return Task.FromResult(new InactiveCategoryResponse(request.Id));
        }
    }
    public record InactiveCategoryRequest : IRequest<InactiveCategoryResponse>
    {
        public long Id { get; set; }
    }
    public record InactiveCategoryResponse
    {
        private long Id { get; set; }

        public string Message { get; set; }

        public InactiveCategoryResponse(long id)
        {
            this.Id = id;
            this.Message = "¡Operación Exitosa!";
        }
    }

    public class InactiveCategoryValidator : AbstractValidator<InactiveCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        private Category _category;
        public InactiveCategoryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Id).Must(ExistCategory).WithMessage($"Categoria no encontrada");
            RuleFor(x => x.Id).Must(ExistProductCategory).WithMessage($"No se puede eliminar esta categoria porque tiene productos registrados");

        }
        private bool ExistCategory(long id)
        {
            _category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t => t.Id == id, includeProperties: "Products");
            return _category != null;
        }
        private bool ExistProductCategory(long id)
        {
            return _category.Products.Where(t=> t.Status !="IN").Count() == 0;
        }
    }
}
