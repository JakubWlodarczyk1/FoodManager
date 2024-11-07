using FoodManager.Domain.Entities;
using FoodManager.Domain.Interfaces;
using FoodManager.Infrastructure.Persistence;

namespace FoodManager.Infrastructure.Repositories
{
    internal class CategoryRepository(FoodManagerDbContext dbContext) : ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetUserAccessibleCategories()
        {
            throw new NotImplementedException();
        }
    }
}
