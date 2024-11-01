using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await productRepository.Delete(request.Id);
        }
    }
}
