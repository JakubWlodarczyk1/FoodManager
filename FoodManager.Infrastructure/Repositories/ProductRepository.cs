using FoodManager.Domain.Interfaces;
using FoodManager.Domain.Entities;
using FoodManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodManager.Infrastructure.Repositories
{
    internal class ProductRepository(FoodManagerDbContext dbContext) : IProductRepository
    {
        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public async Task Create(Product product)
        {
            dbContext.Add(product);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product to update.</param>
        public async Task Update(Product product)
        {
            dbContext.Update(product);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        public async Task Delete(Product product)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The <see cref="Product"/>, or null if not found.</returns>
        public async Task<Product?> GetById(int id)
        {
            return await dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>A collection of all <see cref="Product"/>.</returns>
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await dbContext.Products.ToListAsync();
        }

        /// <summary>
        /// Retrieves products created by the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of <see cref="Product"/> created by the user.</returns>
        public async Task<IEnumerable<Product>> GetUserProducts(string userId)
        {
            return await dbContext.Products.Where(p => p.CreatedById == userId).Include(p => p.Category).ToListAsync();
        }
    }
}
