using MediatR;

namespace FoodManager.Application.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand(int id) : IRequest
    {
        public int Id { get; set; } = id;
    }
}
