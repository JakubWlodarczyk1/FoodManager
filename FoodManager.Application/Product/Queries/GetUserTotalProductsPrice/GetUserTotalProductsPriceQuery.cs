using FoodManager.Application.Product.Dtos;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserTotalProductsPrice
{
    public class GetUserTotalProductsPriceQuery : IRequest<TotalProductsPriceDto>
    {
    }
}
