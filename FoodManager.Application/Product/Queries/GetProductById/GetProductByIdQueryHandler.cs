using AutoMapper;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        /// <summary>
        /// Handles getting a product based on provided query.
        /// </summary>
        /// <param name="request">The query request containing the product ID.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The <see cref="ProductDto"/> representing the requested product.</returns>
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product =  await productRepository.GetById(request.Id);
            var dto = mapper.Map<ProductDto>(product);

            return dto;
        }
    }
}
