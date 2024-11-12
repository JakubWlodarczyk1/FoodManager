using FoodManager.Application.Common;
using FoodManager.Application.Product.Dtos;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserProducts
{
    public class GetUserProductsQuery : IRequest<PagedResult<ProductDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
