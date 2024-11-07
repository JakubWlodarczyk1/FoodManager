using Microsoft.AspNetCore.Mvc;
using FoodManager.Infrastructure.Localization;
using System.Globalization;

namespace FoodManager.MVC.Controllers
{
    public class LocalizationController : Controller
    {
        /// <summary>
        /// Changes the application's language based on the provided culture code.
        /// </summary>
        /// <param name="lang">The culture code to switch to.</param>
        public IActionResult ChangeLanguage(string lang)
        {
            if (string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pl");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl");
                lang = "pl";
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }

            Response.Cookies.Append("Language", lang);
            return Redirect(Request.GetTypedHeaders().Referer?.ToString()!);
        }

        /// <summary>
        /// Get all translations for the specified culture.
        /// </summary>
        /// <param name="culture">The culture code.</param>
        public IActionResult GetTranslations(string culture)
        {
            var translations = TranslationHelper.GetTranslations(culture);
            return Json(translations);
        }
    }
}
