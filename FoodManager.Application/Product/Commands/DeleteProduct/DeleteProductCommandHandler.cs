using FoodManager.Application.ApplicationUser;
using FoodManager.Domain.Entities;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository productRepository, IUserContext userContext) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.Id);
            if (product == null)
                return;

            var user = userContext.GetCurrentUser();
            var canDelete = product.CreatedById == user.Id;

            if (!canDelete)
                return;

            await productRepository.Delete(product);
        }
    }
}
