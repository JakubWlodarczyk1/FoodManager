using FoodManager.Application.ApplicationUser;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository productRepository, IUserContext userContext) : IRequestHandler<DeleteProductCommand>
    {
        /// <summary>
        /// Handles the deletion of a product based on the provided command.
        /// </summary>
        /// <param name="request">The command containing the product ID to delete.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Unit"/> task representing the asynchronous operation.</returns>
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
