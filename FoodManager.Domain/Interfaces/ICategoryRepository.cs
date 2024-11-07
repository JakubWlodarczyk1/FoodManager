using FoodManager.Domain.Entities;

namespace FoodManager.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetUserAccessibleCategories();
    }
}
