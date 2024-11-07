using FoodManager.Application.ProductCategory.Dtos;
using MediatR;

namespace FoodManager.Application.ProductCategory.Queries.GetUserProductCategories
{
    public class GetUserProductCategoriesQuery : IRequest<IEnumerable<ProductCategoryDto>>
    {
    }
}
