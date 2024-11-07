using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.ProductCategory.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.ProductCategory.Queries.GetUserProductCategories
{
    public class GetUserProductCategoriesQueryHandler(IProductCategoryRepository productCategoryRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetUserProductCategoriesQuery, IEnumerable<ProductCategoryDto>>
    {
        /// <summary>
        /// Handles the retrieval of product categories for the current user based on the provided query.
        /// </summary>
        /// <param name="request">The query containing request details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A collection of <see cref="ProductCategoryDto"/> representing the user's product categories.</returns>
        public async Task<IEnumerable<ProductCategoryDto>> Handle(GetUserProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            var productCategories = await productCategoryRepository.GetUserProductCategories(currentUser.Id);
            var dtos = mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);

            return dtos;
        }
    }
}
