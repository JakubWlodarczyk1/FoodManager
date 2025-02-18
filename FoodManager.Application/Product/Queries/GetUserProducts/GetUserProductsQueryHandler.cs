﻿using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Common;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserProducts
{
    public class GetUserProductsQueryHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetUserProductsQuery, PagedResult<ProductDto>>
    {
        /// <summary>
        /// Handles getting a paginated list of products for the current user based on the provided query.
        /// </summary>
        /// <param name="request">The query request containing search phrase and pagination parameters.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="PagedResult{ProductDto}"/> containing a paginated collection of <see cref="ProductDto"/> associated with the current user.</returns>
        public async Task<PagedResult<ProductDto>> Handle(GetUserProductsQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var convertedCategoryIds = request.CategoryIds?.Select(id => id == -1 ? (int?)null : id).ToArray();

            var (products, totalCount) = await productRepository.GetUserProductsMatchingSearch(user.Id, request.SearchPhrase, request.PageNumber, request.PageSize, request.SortBy, request.SortDirection, convertedCategoryIds);

            var dtos = mapper.Map<IEnumerable<ProductDto>>(products);

            var result = new PagedResult<ProductDto>(dtos, totalCount, request.PageNumber, request.PageSize);

            return result;
        }
    }
}
