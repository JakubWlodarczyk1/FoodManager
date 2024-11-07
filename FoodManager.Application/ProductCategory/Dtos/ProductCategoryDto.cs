using FoodManager.Application.Resources.Localizations;
using System.ComponentModel.DataAnnotations;

namespace FoodManager.Application.ProductCategory.Dtos
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Lang))]
        public string Name { get; set; } = default!;
        public string? TranslationKey { get; set; }
    }
}
