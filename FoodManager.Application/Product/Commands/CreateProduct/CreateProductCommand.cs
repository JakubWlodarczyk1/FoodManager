using FoodManager.Application.Product.Dtos;
using MediatR;

namespace FoodManager.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommand : ProductDto, IRequest
    {
    }
}
