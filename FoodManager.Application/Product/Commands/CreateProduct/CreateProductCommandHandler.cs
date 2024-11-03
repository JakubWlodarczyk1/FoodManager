using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<CreateProductCommand>
    {
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Domain.Entities.Product>(request);

            product.CreatedById = userContext.GetCurrentUser().Id;

            await productRepository.Create(product);
        }
    }
}
