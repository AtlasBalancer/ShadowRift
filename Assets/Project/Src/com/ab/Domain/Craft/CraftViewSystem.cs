using System;
using System.Threading;
using com.ab.common;
using com.ab.complexity.core;
using com.ab.core;
using com.ab.domain.item;
using com.ab.domain.price;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;

namespace com.ab.domain.craft
{
    public class CraftViewSystem : ViewPresenter<CraftMono>, IPreInitLoad, IInitSystem, IUpdateSystem
    {
        public CraftViewSystem(Settings def)
        {
            _def = def;
            _atlas = W.Context<AtlasService>.Get();
            _localization = W.Context<LocalizationService>.Get();
        }

        readonly Settings _def;
        readonly AtlasService _atlas;
        readonly LocalizationService _localization;

        public UniTask PreInitLoad(CancellationToken ct) =>
            _atlas.LoadAtlas(_def.AtlasKey);

        public void Init()
        {
            base.Init(_def.CraftPrefab, _def.Root, _def.CraftBtn);

            foreach (var entC in WC.Query.Entities<All<CraftEntry>>())
            {
                var item = UnityEngine.Object.Instantiate(_def.ItemPrefab);
                var itemDef = entC.Ref<ItemEntry>();

                item.Init(entC, true);
                item.UpdateIcon(_atlas.GetSprite(_def.AtlasKey, itemDef.AKSprite));
                item.UpdateTile(_localization.GetString(itemDef.LKTitle, _def.LocalizationTable));
                item.UpdateDescription(_localization.GetString(itemDef.LKDescription, _def.LocalizationTable));
                View.AddItem(item.transform);
            }
        }

        protected override void Show()
        {
            ActiveCraftItems(true);
            base.Show();
        }

        protected override void Hide()
        {
            ActiveCraftItems(false);
            base.Hide();
        }

        void ActiveCraftItems(bool active)
        {
            foreach (var ent in W.Query.Entities<All<CraftItemRef>>()) 
                ent.ApplyTag<ActiveTag>(active);
        }

        public void Update()
        {
            if (!IsActive())
                return;

            foreach (var ent in W.Query.Entities<All<CraftItemRef>, TagAll<Click>>())
            {
                ent.ApplyTag<InventoryAdd>(true);
                ent.ApplyTag<Click>(false);
            }
        }

        [Serializable]
        public class Settings
        {
            public RectTransform Root;
            public Button CraftBtn;
            public CraftMono CraftPrefab;
            public PriceItemMono PricePrefab;
            public CraftItemMono ItemPrefab;
            public string AtlasKey = "ItemsAtlas";
            public string LocalizationTable = "ShadowRiftItems";
        }
    }
}