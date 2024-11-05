using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserProducts
{
    public class GetUserProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}
