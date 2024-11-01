using System.ComponentModel.DataAnnotations;

namespace FoodManager.Domain.Enums
{
    public enum Unit
    {
        [Display(Name = "szt.")]
        Pieces,

        [Display(Name = "g")]
        Grams,

        [Display(Name = "ml")]
        Milliliters
    }
}
