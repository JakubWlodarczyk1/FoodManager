using FoodManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodManager.Infrastructure.Persistence
{
    public class FoodManagerDbContext(DbContextOptions<FoodManagerDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Unit)
                .HasConversion<string>();
        }
    }
}
