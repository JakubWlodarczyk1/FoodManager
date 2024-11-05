using FoodManager.Domain.Entities;

namespace FoodManager.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);
        Task<Product?> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetUserProducts(string userId);
    }
}
