using FoodManager.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodManager.Infrastructure.Persistence
{
    public class FoodManagerDbContext(DbContextOptions<FoodManagerDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Unit)
                .HasConversion<string>();
        }
    }
}
