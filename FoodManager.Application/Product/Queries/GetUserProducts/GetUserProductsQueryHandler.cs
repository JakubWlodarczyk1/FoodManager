using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserProducts
{
    public class GetUserProductsQueryHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<GetUserProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetUserProductsQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var products = await productRepository.GetUserProducts(user.Id);

            var dtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return dtos;
        }
    }
}
