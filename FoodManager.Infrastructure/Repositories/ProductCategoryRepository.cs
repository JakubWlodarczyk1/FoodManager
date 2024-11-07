using FoodManager.Domain.Entities;
using FoodManager.Domain.Enums;
using FoodManager.Domain.Interfaces;
using FoodManager.Infrastructure.Localization;
using FoodManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodManager.Infrastructure.Repositories
{
    internal class ProductCategoryRepository(FoodManagerDbContext dbContext) : IProductCategoryRepository
    {
        /// <summary>
        /// Adds a new product category to the database.
        /// </summary>
        /// <param name="productCategory">The product category to add.</param>
        public async Task Create(ProductCategory productCategory)
        {
            dbContext.ProductCategories.Add(productCategory);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves product categories associated with the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of <see cref="ProductCategory"/>.</returns>
        public async Task<IEnumerable<ProductCategory>> GetUserProductCategories(string userId)
        {
            return await dbContext.ProductCategories.Where(c => c.Type == CategoryType.Base || c.CreatedById == userId).ToListAsync();
        }

        /// <summary>
        /// Retrieves a product category by name and user ID.
        /// </summary>
        /// <param name="name">The name of the product category.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The matching <see cref="ProductCategory"/>, or null if not found.</returns>
        public async Task<ProductCategory?> GetByNameAndUserId(string name, string userId)
        {
            var categories = await GetUserProductCategories(userId);

            return categories.FirstOrDefault(c => (string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase) || TranslationHelper.IsTranslationEqualInAnyDefinedCulture(c.TranslationKey, name)) && (c.Type == CategoryType.Base || c.CreatedById == userId));
        }
    }
}
