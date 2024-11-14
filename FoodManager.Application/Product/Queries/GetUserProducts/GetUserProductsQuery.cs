using FoodManager.Application.Common;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Enums;
using MediatR;

namespace FoodManager.Application.Product.Queries.GetUserProducts
{
    public class GetUserProductsQuery : IRequest<PagedResult<ProductDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = PaginationDefaults.DefaultPageNumber;
        public int PageSize { get; set; } = PaginationDefaults.DefaultPageSize;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public int[]? CategoryIds { get; set; }
    }
}
