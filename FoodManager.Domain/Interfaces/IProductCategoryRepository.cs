using FoodManager.Domain.Entities;

namespace FoodManager.Domain.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task Create(ProductCategory productCategory);
        Task<IEnumerable<ProductCategory>> GetUserProductCategories(string userId);
        Task<ProductCategory?> GetByNameAndUserId(string name, string userId);
    }
}
