using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserTotalProductsPrice
{
    public class GetUserTotalProductsPriceQueryHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetUserTotalProductsPriceQuery, TotalProductsPriceDto>
    {
        /// <summary>
        /// Handles getting the total price of all products for the current user based on the provided query.
        /// </summary>
        /// <param name="request">The query containing parameters for retrieving the total products price.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The <see cref="TotalProductsPriceDto"/> containing the total price of the user's products.</returns>
        public async Task<TotalProductsPriceDto> Handle(GetUserTotalProductsPriceQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var totalPrice = await productRepository.GetUserTotalProductsPrice(user.Id);
            var dto = mapper.Map<TotalProductsPriceDto>(totalPrice);

            return dto;
        }
    }
}
