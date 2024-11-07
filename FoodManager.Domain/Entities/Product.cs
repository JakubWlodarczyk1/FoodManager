using FoodManager.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace FoodManager.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public required string CreatedById { get; set; }
        public required IdentityUser CreatedBy { get; set; }
    }
}
