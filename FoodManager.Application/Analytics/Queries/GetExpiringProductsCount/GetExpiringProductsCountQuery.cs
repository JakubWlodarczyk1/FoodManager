using FoodManager.Application.Product.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodManager.Application.Analytics.Dtos;

namespace FoodManager.Application.Analytics.Queries.GetExpiringProductsCount
{
    public class GetExpiringProductsCountQuery : IRequest<ExpiringProductsCountDto>
    {
        public DateOnly From { get; init; }
        public DateOnly To { get; init; }
        public int[]? CategoryIds { get; init; }
        public bool IncludeOutOfStock { get; init; } = false;
    }
}
