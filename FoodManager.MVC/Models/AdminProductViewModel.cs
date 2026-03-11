using FoodManager.Domain.Enums;

namespace FoodManager.MVC.Models
{
    public class AdminProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public string? CategoryName { get; set; }
        public string? CategoryTranslationKey { get; set; }

        public decimal Quantity { get; set; }
        public Unit Unit { get; set; }

        public decimal? Price { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string? CreatedByEmail { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
