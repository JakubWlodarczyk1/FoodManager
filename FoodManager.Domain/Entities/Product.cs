using FoodManager.Domain.Enums;

namespace FoodManager.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
