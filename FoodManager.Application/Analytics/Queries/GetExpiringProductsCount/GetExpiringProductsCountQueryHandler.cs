using AutoMapper;
using FoodManager.Application.Analytics.Dtos;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Product.Dtos;
using FoodManager.Application.Product.Queries.GetUserTotalProductsPrice;
using FoodManager.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodManager.Application.Analytics.Queries.GetExpiringProductsCount
{
    public class GetExpiringProductsCountQueryHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetExpiringProductsCountQuery, ExpiringProductsCountDto>
    {
        public async Task<ExpiringProductsCountDto> Handle(GetExpiringProductsCountQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();

            int?[]? convertedCategoryIds = request.CategoryIds?
                .Select(id => id == -1 ? (int?)null : id)
                .ToArray();

            var dailyCounts = await productRepository.GetExpiringProductsDailyCounts(
                userId: user.Id,
                from: request.From,
                to: request.To,
                categoryIds: convertedCategoryIds,
                includeOutOfStock: request.IncludeOutOfStock,
                cancellationToken: cancellationToken
            );

            var days = request.To.DayNumber - request.From.DayNumber + 1;
            var dict = dailyCounts.ToDictionary(x => x.Date, x => x.Count);

            var points = new List<ExpiringProductsCountPointDto>(days);
            var cursor = request.From;
            var total = 0;

            for (int i = 0; i < days; i++)
            {
                var value = dict.TryGetValue(cursor, out var c) ? c : 0;
                points.Add(new ExpiringProductsCountPointDto { Date = cursor, Count = value });
                total += value;
                cursor = cursor.AddDays(1);
            }

            return new ExpiringProductsCountDto
            {
                Points = points,
                Total = total
            };
        }
    }
}
