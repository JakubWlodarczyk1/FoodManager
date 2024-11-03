using FoodManager.Domain.Interfaces;
using FoodManager.Domain.Entities;
using FoodManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodManager.Infrastructure.Repositories
{
    internal class ProductRepository(FoodManagerDbContext dbContext) : IProductRepository
    {
        public async Task Create(Product product)
        {
            dbContext.Add(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            dbContext.Update(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await dbContext.Products.ToListAsync();
        }
    }
}
