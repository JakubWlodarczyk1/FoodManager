using AutoMapper;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        /// <summary>
        /// Processes the <see cref="GetAllProductsQuery"/> and returns a list of <see cref="ProductDto"/>.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A collection of <see cref="ProductDto"/>.</returns>
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAll();
            var dtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return dtos;
        }
    }
}
