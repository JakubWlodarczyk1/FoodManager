using FoodManager.Application.ProductCategory.Dtos;
using MediatR;

namespace FoodManager.Application.ProductCategory.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand : ProductCategoryDto, IRequest<ProductCategoryResultDto>
    {
    }
}
