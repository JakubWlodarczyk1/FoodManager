using FluentValidation;
using FoodManager.Application.Extensions;
using FoodManager.Domain.Interfaces;

namespace FoodManager.Application.Product.Commands.EditProduct
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator(IProductRepository repository)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Proszę wprowadzić nazwę produktu")
                .MinimumLength(2).WithMessage("Nazwa produktu musi mieć co najmniej 2 znaki")
                .MaximumLength(40).WithMessage("Nazwa produktu może mieć maksymalnie 40 znaków");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("Proszę podać ilość produktu")
                .InclusiveBetween(1, 10000).WithMessage("Ilość produktu musi mieścić się w zakresie od 1 do 10000");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Proszę podać cenę produktu")
                .InclusiveBetween(0, 10000).WithMessage("Cena produktu musi mieścić się w zakresie od 0 do 10,000");

            RuleFor(p => p.ExpirationDate)
                .NotEmpty().WithMessage("Proszę podać datę ważności")
                .Must(DateTimeExtensions.IsTodayOrInTheFuture).WithMessage("Data ważności musi być dzisiejsza lub przyszła");
        }
    }
}
