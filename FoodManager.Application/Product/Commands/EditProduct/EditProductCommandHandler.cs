using AutoMapper;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Commands.EditProduct
{
    public class EditProductCommandHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<EditProductCommand>
    {
        public async Task Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.Id);
            if (product == null)
                return;

            mapper.Map(request, product);

            await productRepository.Update(product);
        }
    }
}
