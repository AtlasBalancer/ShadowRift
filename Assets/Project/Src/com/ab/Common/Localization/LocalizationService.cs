using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace com.ab.common
{
    public class LocalizationService
    {
        public event Action OnLocaleChanged;

        public LocalizationService()
        {
            LocalizationSettings.SelectedLocaleChanged += _ => OnLocaleChanged?.Invoke();
        }

        // ── Инициализация ────────────────────────────────────────────────────

        public async UniTask InitializeAsync()
        {
            await LocalizationSettings.InitializationOperation.ToUniTask();
        }

        // ── Строки ───────────────────────────────────────────────────────────

        /// <summary>Возвращает локализованную строку синхронно (только если таблица уже загружена).</summary>
        public string GetString(string table, string key)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString(table, key);
        }

        /// <summary>Возвращает локализованную строку асинхронно.</summary>
        public async UniTask<string> GetStringAsync(string table, string key)
        {
            var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(table, key);
            return await op.ToUniTask();
        }

        // ── Ассеты ───────────────────────────────────────────────────────────

        /// <summary>Возвращает локализованный ассет асинхронно.</summary>
        public async UniTask<T> GetAssetAsync<T>(string table, string key) where T : UnityEngine.Object
        {
            var op = LocalizationSettings.AssetDatabase.GetLocalizedAssetAsync<T>(table, key);
            return await op.ToUniTask();
        }

        // ── Локаль ───────────────────────────────────────────────────────────

        public Locale CurrentLocale => LocalizationSettings.SelectedLocale;

        /// <summary>Меняет язык по коду (например "ru", "en").</summary>
        public void SetLocale(string localeCode)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;
            foreach (var locale in locales)
            {
                if (!locale.Identifier.Code.Equals(localeCode, StringComparison.OrdinalIgnoreCase))
                    continue;

                LocalizationSettings.SelectedLocale = locale;
                return;
            }

            throw new ArgumentException($"{nameof(LocalizationService)}:: Locale '{localeCode}' not found");
        }

        public void SetLocale(Locale locale)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
    }
}
