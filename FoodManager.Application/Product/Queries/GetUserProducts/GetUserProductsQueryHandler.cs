using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserProducts
{
    public class GetUserProductsQueryHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetUserProductsQuery, IEnumerable<ProductDto>>
    {
        /// <summary>
        /// Processes the <see cref="GetUserProductsQuery"/> and returns a list of <see cref="ProductDto"/> for the current user.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A collection of <see cref="ProductDto"/> associated with the current user.</returns>
        public async Task<IEnumerable<ProductDto>> Handle(GetUserProductsQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var products = await productRepository.GetUserMatchingProducts(user.Id, request.SearchPhrase);

            var dtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return dtos;
        }
    }
}
