using AutoMapper;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<CreateProductCommand>
    {
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Domain.Entities.Product>(request);
            await productRepository.Create(product);
        }
    }
}
