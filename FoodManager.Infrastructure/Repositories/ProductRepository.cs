using FoodManager.Application.Common;
using FoodManager.Domain.Interfaces;
using FoodManager.Domain.Entities;
using FoodManager.Domain.Enums;
using FoodManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodManager.Infrastructure.Repositories
{
    internal class ProductRepository(FoodManagerDbContext dbContext) : IProductRepository
    {
        /// <summary>
        /// Add a new product to the database.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public async Task Create(Product product)
        {
            dbContext.Add(product);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update an existing product in the database.
        /// </summary>
        /// <param name="product">The product to update.</param>
        public async Task Update(Product product)
        {
            dbContext.Update(product);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a product from the database.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        public async Task Delete(Product product)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The <see cref="Product"/>, or null if not found.</returns>
        public async Task<Product?> GetById(int id)
        {
            return await dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Get products created by the specified user, optionally filtered by a search phrase, 
        /// with support for pagination, sorting, and category filtering.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="searchPhrase">The optional search phrase to filter products by name or description.</param>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="sortBy">The property by which to sort the products. Use <c>null</c> to disable sorting and return the products in their default order.</param>
        /// <param name="sortDirection">The direction of sorting.</param>
        /// <param name="categoryIds">Optional array of category IDs to filter products by. Use <c>null</c> for no category filtering, and include <c>null</c> in the array to include uncategorized products.</param>
        /// <returns>A tuple containing a collection of <see cref="Product"/> that match the search criteria and the total count of matching products.</returns>
        public async Task<(IEnumerable<Product>, int)> GetUserProductsMatchingSearch(string userId, string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection, int?[]? categoryIds)
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = dbContext.Products
                .Where(p =>
                    p.CreatedById == userId &&
                    (categoryIds == null || categoryIds.Contains(p.CategoryId)) &&
                    (
                        searchPhraseLower == null ||
                        (
                            p.Name.ToLower().Contains(searchPhraseLower) ||
                            (p.Description != null && p.Description.ToLower().Contains(searchPhraseLower))
                        )
                    )
                );

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var selectedColumn =  ProductSortingConfiguration.ColumnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }

            var products = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Include(p => p.Category)
                .ToListAsync();

            return (products, totalCount);
        }

        /// <summary>
        /// Get the total price of all products created by the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The total price of the user's products.</returns>
        public async Task<decimal> GetUserTotalProductsPrice(string userId)
        {
            return await dbContext.Products.Where(p => p.CreatedById == userId).SumAsync(p => p.Price) ?? default;
        }

        public async Task<IReadOnlyList<(DateOnly Date, int Count)>> GetExpiringProductsDailyCounts(string userId, DateOnly from, DateOnly to, int?[]? categoryIds, bool includeOutOfStock, CancellationToken cancellationToken)
        {
            var fromDt = from.ToDateTime(TimeOnly.MinValue);
            var toDt = to.ToDateTime(TimeOnly.MaxValue);

            var q = dbContext.Products
                .AsNoTracking()
                .Where(p => p.CreatedById == userId)
                .Where(p => p.ExpirationDate >= fromDt && p.ExpirationDate <= toDt);

            if (categoryIds is { Length: > 0 })
            {
                var hasNull = categoryIds.Any(id => id is null);
                var nonNull = categoryIds.Where(id => id is not null).Select(id => id!.Value).ToArray();

                if (hasNull && nonNull.Length > 0)
                    q = q.Where(p => (p.CategoryId == null) || (nonNull.Contains(p.CategoryId.Value)));
                else if (hasNull)
                    q = q.Where(p => p.CategoryId == null);
                else
                    q = q.Where(p => p.CategoryId != null && nonNull.Contains(p.CategoryId.Value));
            }

            if (!includeOutOfStock)
                q = q.Where(p => p.Quantity > 0);

            var data = await q
                .GroupBy(p => DateOnly.FromDateTime(p.ExpirationDate))
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Sum(p => p.Unit == Domain.Enums.Unit.Pieces ? (int)p.Quantity : 1)
                })
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);

            return data.Select(x => (x.Date, x.Count)).ToList();
        }
    }
}