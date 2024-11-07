using System.ComponentModel.DataAnnotations;
using FoodManager.Application.Resources.Localizations;
using FoodManager.Domain.Enums;

namespace FoodManager.Application.Product.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Lang))]
        public string Name { get; set; } = default!;

        [Display(Name = "Description", ResourceType = typeof(Lang))]
        public string? Description { get; set; }

        public int? CategoryId { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Lang))]
        public string? CategoryName { get; set; }

        public string? CategoryTranslationKey { get; set; }

        [Display(Name = "Quantity", ResourceType = typeof(Lang))]
        public int Quantity { get; set; }

        [Display(Name = "Unit", ResourceType = typeof(Lang))]
        public Unit Unit { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Lang))]
        public decimal Price { get; set; }

        [Display(Name = "ExpirationDate", ResourceType = typeof(Lang))]
        public DateTime ExpirationDate { get; set; }

        public bool IsEditable { get; set; }
    }
}
