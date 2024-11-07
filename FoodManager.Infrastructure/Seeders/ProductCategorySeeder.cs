using FoodManager.Domain.Entities;
using FoodManager.Domain.Enums;
using FoodManager.Infrastructure.Persistence;

namespace FoodManager.Infrastructure.Seeders
{
    public class ProductCategorySeeder(FoodManagerDbContext dbContext)
    {
        private readonly ProductCategory[] _baseCategories = 
        [
            new ProductCategory { Name = "fruits", TranslationKey = "Fruits", Type = CategoryType.Base },
            new ProductCategory { Name = "vegetables", TranslationKey = "Vegetables", Type = CategoryType.Base },
            new ProductCategory { Name = "dairy", TranslationKey = "Dairy", Type = CategoryType.Base },
            new ProductCategory { Name = "meat", TranslationKey = "Meat", Type = CategoryType.Base },
            new ProductCategory { Name = "snacks", TranslationKey = "Snacks", Type = CategoryType.Base },
            new ProductCategory { Name = "protein", TranslationKey = "Protein", Type = CategoryType.Base },
            new ProductCategory { Name = "oils", TranslationKey = "Oils", Type = CategoryType.Base },
        ];

        /// <summary>
        /// Seeds the database with base product categories if none exist.
        /// </summary>
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.ProductCategories.Any())
                {
                    await dbContext.ProductCategories.AddRangeAsync(_baseCategories);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
