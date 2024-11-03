using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.Product.Commands.EditProduct
{
    public class EditProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<EditProductCommand>
    {
        public async Task Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.Id);
            if (product == null)
                return;

            var user = userContext.GetCurrentUser();
            var isEditable = product.CreatedById == user.Id;

            if (!isEditable)
                return;

            mapper.Map(request, product);

            await productRepository.Update(product);
        }
    }
}
