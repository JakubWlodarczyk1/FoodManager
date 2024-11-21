using FluentValidation;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Resources.Localizations;
using FoodManager.Domain.Interfaces;

namespace FoodManager.Application.ProductCategory.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductCategoryCommandValidator"/> class and sets up validation rules.
        /// </summary>
        /// <param name="repository">Repository for product category data operations.</param>
        /// <param name="userContext">Current user context.</param>
        public CreateProductCategoryCommandValidator(IProductCategoryRepository repository, IUserContext userContext)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(string.Format(Lang.RequiredFieldMessage, Lang.Name))
                .MinimumLength(2).WithMessage(Lang.MinLengthValidationMessage)
                .MaximumLength(40).WithMessage(Lang.MaxLengthValidationMessage)
                .Custom((value, context) =>
                {
                    var user = userContext.GetCurrentUser();
                    var existingCategory = repository.GetByNameAndUserId(value, user.Id).Result;

                    if (existingCategory != null)
                    {
                        context.AddFailure(string.Format(Lang.CategoryAlreadyExistsMessage, value));
                    }
                });
        }
    }
}
