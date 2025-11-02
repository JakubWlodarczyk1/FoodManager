using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Application.Analytics.Queries.GetExpiringProductsCount
{
    public class GetExpiringProductsCountQueryValidator : AbstractValidator<GetExpiringProductsCountQuery>
    {
        public GetExpiringProductsCountQueryValidator()
        {
            RuleFor(x => x.To)
                .GreaterThanOrEqualTo(x => x.From)
                .WithMessage("'To' cannot be earlier than 'From'.");

            RuleFor(x => x)
                .Must(x => (x.To.DayNumber - x.From.DayNumber + 1) is > 0 and <= 366)
                .WithMessage("Date range is invalid (max 366 days).");
        }
    }
}
