using FoodManager.Application.Product.Dtos;
using System.Linq.Expressions;

namespace FoodManager.Application.Common
{
    public static class ProductSortingConfiguration
    {
        public static readonly string[] AllowedSortByColumnNames =
        [
            nameof(ProductDto.Name),
            nameof(ProductDto.Description),
            nameof(ProductDto.Quantity),
            nameof(ProductDto.Price),
            nameof(ProductDto.ExpirationDate)
        ];

        public static readonly Dictionary<string, Expression<Func<Domain.Entities.Product, object>>> ColumnsSelector = new()
        {
            { nameof(Domain.Entities.Product.Name), p => p.Name },
            { nameof(Domain.Entities.Product.Description), p => p.Description },
            { nameof(Domain.Entities.Product.Quantity), p => p.Quantity },
            { nameof(Domain.Entities.Product.Price), p => p.Price },
            { nameof(Domain.Entities.Product.ExpirationDate), p => p.ExpirationDate }
        };
    }
}
