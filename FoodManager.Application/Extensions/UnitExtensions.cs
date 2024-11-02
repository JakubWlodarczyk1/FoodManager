using FoodManager.Domain.Enums;
using FoodManager.Application.Resources.Localizations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodManager.Application.Extensions
{
    public static class UnitExtensions
    {
        public static string GetDisplayName(this Unit unit)
        {
            return unit switch
            {
                Unit.Pieces => Lang.PiecesShortcut,
                Unit.Grams => Lang.GramsShortcut,
                Unit.Milliliters => Lang.MillilitersShortcut,
                _ => unit.ToString(),
            };
        }

        public static IEnumerable<SelectListItem> GetUnitSelectList()
        {
            return Enum.GetValues(typeof(Unit))
                .Cast<Unit>()
                .Select(u => new SelectListItem
                {
                    Text = u.GetDisplayName(),
                    Value = u.ToString()
                });
        }
    }
}
