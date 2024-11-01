using MediatR;

namespace FoodManager.Application.Product.Queries.GetProductById
{
    public class GetProductByIdQuery(int id) : IRequest<ProductDto>
    {
        public int Id { get; set; } = id;
    }
}
