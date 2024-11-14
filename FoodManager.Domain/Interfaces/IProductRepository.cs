using FoodManager.Domain.Entities;
using FoodManager.Domain.Enums;

namespace FoodManager.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);
        Task<Product?> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<(IEnumerable<Product>, int)> GetUserProductsMatchingSearch(string userId, string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection, int?[]? categoryIds);
    }
}
