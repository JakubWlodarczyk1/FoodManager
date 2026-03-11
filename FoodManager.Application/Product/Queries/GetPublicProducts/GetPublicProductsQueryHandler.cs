using FoodManager.Application.Common;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Application.Product.Queries.GetPublicProducts
{
    public class GetPublicProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetPublicProductsQuery, PagedResult<PublicProductDto>>
    {
        public async Task<PagedResult<PublicProductDto>> Handle(GetPublicProductsQuery request, CancellationToken cancellationToken)
        {
            var convertedCategoryIds = request.CategoryIds?.Select(id => id == -1 ? (int?)null : id).ToArray();

            var (products, totalCount) = await productRepository.GetProductsMatchingSearch(
                request.SearchPhrase,
                request.PageNumber,
                request.PageSize,
                request.SortBy,
                request.SortDirection,
                convertedCategoryIds);

            var dtos = products.Select(p => new PublicProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name,
                CategoryTranslationKey = p.Category?.TranslationKey,
                Quantity = p.Quantity,
                Unit = p.Unit,
                Price = p.Price,
                ExpirationDate = p.ExpirationDate
            });

            return new PagedResult<PublicProductDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
