using FluentValidation;
using FoodManager.Application.Extensions;
using FoodManager.Application.Resources.Localizations;
using FoodManager.Domain.Interfaces;

namespace FoodManager.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator(IProductRepository repository)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(Lang.RequiredValidationMessage)
                .MinimumLength(2).WithMessage(Lang.MinLengthValidationMessage)
                .MaximumLength(40).WithMessage(Lang.MaxLengthValidationMessage);

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage(Lang.RequiredValidationMessage)
                .InclusiveBetween(1, 10000).WithMessage(Lang.RangeValidationMessage);

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage(Lang.RequiredValidationMessage)
                .InclusiveBetween(0, 10000).WithMessage(Lang.RangeValidationMessage);

            RuleFor(p => p.ExpirationDate)
                .NotEmpty().WithMessage(Lang.RequiredValidationMessage)
                .Must(DateTimeExtensions.IsTodayOrInTheFuture).WithMessage(Lang.ExpirationDateTodayOrFuture);
        }
    }
}
