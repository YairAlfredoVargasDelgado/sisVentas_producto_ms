using FluentValidation;
using FluentValidation.Validators;
using System.Linq;
using SysVentas.Products.Domain.Base;

namespace SysVentas.Products.Application.Base
{
    public static class ValidationResponseConvert
    {

        public static void ToValidationFailure<T>(this DomainValidation validation, ValidationContext<T> context)
        {
            var failures = validation.Fallos.ToList();
            failures.ForEach(error =>
            {
                context.AddFailure(error.Key, error.Value);
            });
        }
    }
}
