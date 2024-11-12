using FluentValidation;
using FoodManager.Application.Common;
using FoodManager.Application.Resources.Localizations;

namespace FoodManager.Application.Product.Queries.GetUserProducts
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserProductsQuery"/> class and sets up validation rules.
    /// </summary>
    public class GetUserProductsQueryValidator : AbstractValidator<GetUserProductsQuery>
    {
        public GetUserProductsQueryValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage(Lang.PageNumberGreaterThanOrEqual);

            RuleFor(p => p.PageSize)
                .Must(value => PaginationDefaults.AllowedPageSizes.Contains(value)).WithMessage(string.Format(Lang.PageSizeInvalid, string.Join(", ", PaginationDefaults.AllowedPageSizes)));
        }
    }
}
