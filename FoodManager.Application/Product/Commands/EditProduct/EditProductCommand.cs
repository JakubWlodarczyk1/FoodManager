using FoodManager.Application.Product.Dtos;
using MediatR;

namespace FoodManager.Application.Product.Commands.EditProduct
{
    public class EditProductCommand : ProductDto, IRequest
    {
    }
}
