using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace com.ab.common
{
    public class LocalizationService
    {
        const string TABLE = "Default";
        
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

        // ── Загрузка таблиц ──────────────────────────────────────────────────

        readonly Dictionary<string, StringTable> _stringTables = new();
        readonly Dictionary<string, AssetTable>  _assetTables  = new();

        /// <summary>Предзагружает String Table по имени и кэширует её.</summary>
        public async UniTask PreloadStringTableAsync(string table)
        {
            if (_stringTables.ContainsKey(table)) return;
            var op = LocalizationSettings.StringDatabase.GetTableAsync(table);
            _stringTables[table] = await op.ToUniTask();
        }

        /// <summary>Предзагружает Asset Table по имени и кэширует её.</summary>
        public async UniTask PreloadAssetTableAsync(string table)
        {
            if (_assetTables.ContainsKey(table)) return;
            var op = LocalizationSettings.AssetDatabase.GetTableAsync(table);
            _assetTables[table] = await op.ToUniTask();
        }

        /// <summary>Возвращает закэшированную String Table (должна быть предзагружена).</summary>
        public StringTable GetStringTable(string table)
        {
            if (_stringTables.TryGetValue(table, out var t)) return t;
            throw new InvalidOperationException($"{nameof(LocalizationService)}:: String table '{table}' not preloaded. Call PreloadStringTableAsync first.");
        }

        /// <summary>Возвращает закэшированную Asset Table (должна быть предзагружена).</summary>
        public AssetTable GetAssetTable(string table)
        {
            if (_assetTables.TryGetValue(table, out var t)) return t;
            throw new InvalidOperationException($"{nameof(LocalizationService)}:: Asset table '{table}' not preloaded. Call PreloadAssetTableAsync first.");
        }

        // ── Строки ───────────────────────────────────────────────────────────

        /// <summary>Возвращает локализованную строку синхронно (только если таблица уже загружена).</summary>
        public string GetString( string key, string table = TABLE)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString(table, key);
        }

        /// <summary>Возвращает локализованную строку асинхронно.</summary>
        public async UniTask<string> GetStringAsync(string key, string table = TABLE)
        {
            var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(table, key);
            return await op.ToUniTask();
        }

        // ── Ассеты ───────────────────────────────────────────────────────────

        /// <summary>Возвращает локализованный ассет асинхронно.</summary>
        public async UniTask<T> GetAssetAsync<T>(string key, string table = TABLE) where T : UnityEngine.Object
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
