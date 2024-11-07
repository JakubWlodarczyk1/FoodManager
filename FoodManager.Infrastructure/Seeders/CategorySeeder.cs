using FoodManager.Domain.Entities;
using FoodManager.Domain.Enums;
using FoodManager.Infrastructure.Persistence;

namespace FoodManager.Infrastructure.Seeders
{
    public class CategorySeeder(FoodManagerDbContext dbContext)
    {
        private readonly Category[] _baseCategories = 
        [
            new Category { Name = "fruits", TranslationKey = "Fruits", Type = CategoryType.Base },
            new Category { Name = "vegetables", TranslationKey = "Vegetables", Type = CategoryType.Base },
            new Category { Name = "dairy", TranslationKey = "Dairy", Type = CategoryType.Base },
            new Category { Name = "meat", TranslationKey = "Meat", Type = CategoryType.Base },
            new Category { Name = "snacks", TranslationKey = "Snacks", Type = CategoryType.Base },
            new Category { Name = "protein", TranslationKey = "Protein", Type = CategoryType.Base },
            new Category { Name = "oils", TranslationKey = "Oils", Type = CategoryType.Base },
        ];

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Categories.Any())
                {
                    await dbContext.Categories.AddRangeAsync(_baseCategories);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
