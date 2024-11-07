using AutoMapper;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        /// <summary>
        /// Processes the <see cref="GetProductByIdQuery"/> and returns the corresponding <see cref="ProductDto"/>.
        /// </summary>
        /// <param name="request">The query request containing the product ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A collection of <see cref="ProductDto"/>.</returns>
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product =  await productRepository.GetById(request.Id);
            var dto = mapper.Map<ProductDto>(product);

            return dto;
        }
    }
}
