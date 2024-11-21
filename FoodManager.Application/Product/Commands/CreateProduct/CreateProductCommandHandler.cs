using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<CreateProductCommand>
    {
        /// <summary>
        /// Handles the creation of a new product based on the provided command.
        /// </summary>
        /// <param name="request">The command containing product details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Domain.Entities.Product>(request);

            product.CreatedById = userContext.GetCurrentUser().Id;
            product.CategoryId = request.CategoryId;

            await productRepository.Create(product);
        }
    }
}
