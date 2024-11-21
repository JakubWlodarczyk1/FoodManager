using FoodManager.Domain.Enums;
using FoodManager.Application.Resources.Localizations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodManager.Application.Extensions
{
    public static class UnitExtensions
    {
        /// <summary>
        /// Retrieves the translated name for the specified unit.
        /// </summary>
        /// <param name="unit">The unit for which to get the translated name.</param>
        /// <returns>Translated name for the specified unit.</returns>
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

        /// <summary>
        /// Generates a list of translated select list items for all available units.
        /// </summary>
        /// <returns>A collection of translated <see cref="SelectListItem"/> representing each unit.</returns>
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
