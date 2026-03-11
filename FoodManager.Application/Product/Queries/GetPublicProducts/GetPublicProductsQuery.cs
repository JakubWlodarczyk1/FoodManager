using FoodManager.Application.Common;
using FoodManager.Application.Product.Dtos;
using FoodManager.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Application.Product.Queries.GetPublicProducts
{
    public class GetPublicProductsQuery : IRequest<PagedResult<PublicProductDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; } = PaginationDefaults.DefaultPageNumber;
        public int PageSize { get; set; } = PaginationDefaults.DefaultPageSize;
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
        public int[]? CategoryIds { get; set; }
    }
}
