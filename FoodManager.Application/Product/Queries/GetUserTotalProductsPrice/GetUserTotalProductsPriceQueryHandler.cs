using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserTotalProductsPrice
{
    public class GetUserTotalProductsPriceQueryHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetUserTotalProductsPriceQuery, TotalProductsPriceDto>
    {
        public async Task<TotalProductsPriceDto> Handle(GetUserTotalProductsPriceQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var totalPrice = await productRepository.GetUserTotalProductsPrice(user.Id);
            var dto = mapper.Map<TotalProductsPriceDto>(totalPrice);

            return dto;
        }
    }
}
