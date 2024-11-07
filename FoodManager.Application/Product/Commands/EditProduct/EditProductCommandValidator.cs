using FluentValidation;
using FoodManager.Application.Extensions;
using FoodManager.Application.Resources.Localizations;

namespace FoodManager.Application.Product.Commands.EditProduct
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditProductCommandValidator"/> class and sets up validation rules.
        /// </summary>
        public EditProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(string.Format(Lang.RequiredFieldMessage, Lang.Name))
                .MinimumLength(2).WithMessage(Lang.MinLengthValidationMessage)
                .MaximumLength(40).WithMessage(Lang.MaxLengthValidationMessage);

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage(string.Format(Lang.RequiredFieldMessage, Lang.Quantity))
                .InclusiveBetween(1, 10000).WithMessage(Lang.RangeValidationMessage);

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage(string.Format(Lang.RequiredFieldMessage, Lang.Price))
                .InclusiveBetween(0, 10000).WithMessage(Lang.RangeValidationMessage);

            RuleFor(p => p.ExpirationDate)
                .NotEmpty().WithMessage(string.Format(Lang.RequiredFieldMessage, Lang.ExpirationDate))
                .Must(DateTimeExtensions.IsTodayOrInTheFuture).WithMessage(Lang.ExpirationDateTodayOrFuture);
        }
    }
}
