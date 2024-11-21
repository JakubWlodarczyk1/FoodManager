using FoodManager.Application.Resources.Localizations;
using System.Collections;
using System.Collections.Concurrent;
using System.Globalization;
using System.Resources;

namespace FoodManager.Infrastructure.Localization
{
    public static class TranslationHelper
    {
        private static readonly ResourceManager ResourceManager = new(typeof(Lang));
        private static readonly ConcurrentDictionary<string, Dictionary<string, string>> Translations = new();
        private static readonly List<CultureInfo> DefinedCultures =
        [
            new CultureInfo("en"),
            new CultureInfo("pl")
        ];

        /// <summary>
        /// Gets all translations as key-value pairs for the specified culture.
        /// </summary>
        /// <param name="culture">The culture identifier.</param>
        /// <returns>The dictionary containing translation key-value pairs for the specified culture.</returns>
        public static Dictionary<string, string> GetTranslations(string culture)
        {
            return Translations.GetOrAdd(culture, _ =>
            {
                var translationDict = new Dictionary<string, string>();
                var resourceSet = ResourceManager.GetResourceSet(new CultureInfo(culture), true, true);

                if (resourceSet == null) 
                    return translationDict;

                foreach (var entry in resourceSet)
                {
                    if (entry is not DictionaryEntry resourceEntry) 
                        continue;

                    var key = resourceEntry.Key?.ToString() ?? string.Empty;
                    var value = resourceEntry.Value?.ToString() ?? string.Empty;

                    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                        continue;

                    translationDict[key] = value;
                }

                return translationDict;
            });
        }

        /// <summary>
        /// Checks if the specified translation key corresponds to the given translation value in any of the defined cultures.
        /// </summary>
        /// <param name="key">The translation key to look up.</param>
        /// <param name="targetTranslation">The translation value to compare.</param>
        /// <returns><c>true</c> if the key's translation matches the specified value in any of the defined cultures; otherwise, <c>false</c>.</returns>
        public static bool IsTranslationEqualInAnyDefinedCulture(string key, string targetTranslation)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(targetTranslation))
                return false;

            foreach (var culture in DefinedCultures)
            {
                var resourceSet = ResourceManager.GetResourceSet(culture, true, true);

                if (resourceSet?.GetObject(key) is string translation && string.Equals(translation, targetTranslation, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the translated value for a given key from resource files.
        /// </summary>
        /// <param name="key">The resource key for the translation.</param>
        /// <returns>The translated string if the key exists; otherwise, <c>null</c>.</returns>
        public static string? GetTranslatedValue(string? key)
        {
            return string.IsNullOrEmpty(key) ? null : Lang.ResourceManager.GetString(key);
        }
    }
}
