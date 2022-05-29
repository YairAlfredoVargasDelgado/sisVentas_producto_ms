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
    public class ActiveCategoryCommand : IRequestHandler<ActiveCategoryRequest, ActiveCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ActiveCategoryRequest> _validator;
        public ActiveCategoryCommand(IUnitOfWork unitOfWork, IValidator<ActiveCategoryRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public Task<ActiveCategoryResponse> Handle(ActiveCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t => t.Id == request.Id);
            category.Active();
            _unitOfWork.Commit();
            return Task.FromResult(new ActiveCategoryResponse(request.Id));
        }
    }
    public record ActiveCategoryRequest : IRequest<ActiveCategoryResponse>
    {        
        public long Id { get; set; }
    }
    public record ActiveCategoryResponse
    {
        private long Id { get; set; }

        public string Message { get; set; }

        public ActiveCategoryResponse(long id)
        {
            this.Id = id;
            this.Message = "¡Operación Exitosa!";
        }
    }

    public class ActiveCategoryValidator : AbstractValidator<ActiveCategoryRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        private Category _category;
        public ActiveCategoryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Id).Must(ExistCategory).WithMessage($"Categoria no encontrada");

        }

        private bool ExistCategory(long id)
        {
            _category = _unitOfWork.CategorysRepository.FindFirstOrDefault(t => t.Id == id);
            return _category != null;
        }
        

    }
}
