using FoodManager.Application.Product.Dtos;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}
