using AutoMapper;
using FoodManager.Application.ApplicationUser;
using FoodManager.Application.ProductCategory.Dtos;
using FoodManager.Domain.Interfaces;
using MediatR;

namespace FoodManager.Application.ProductCategory.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandHandler(IProductCategoryRepository productCategoryRepository, IMapper mapper, IUserContext userContext) : IRequestHandler<CreateProductCategoryCommand, ProductCategoryResultDto>
    {
        /// <summary>
        /// Handles the creation of a new product category based on the provided command.
        /// </summary>
        /// <param name="request">The command containing product category details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The <see cref="ProductCategoryResultDto"/> representing the created product category.</returns>
        public async Task<ProductCategoryResultDto> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var productCategory = mapper.Map<Domain.Entities.ProductCategory>(request);

            productCategory.CreatedById = userContext.GetCurrentUser().Id;

            await productCategoryRepository.Create(productCategory);

            var result = mapper.Map<ProductCategoryResultDto>(productCategory);
            return result;
        }
    }
}
