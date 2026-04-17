using System;
using System.Threading;
using com.ab.common;
using com.ab.complexity.core;
using com.ab.domain.item;
using com.ab.domain.price;
using Cysharp.Threading.Tasks;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace com.ab.domain.craft
{
    public class CraftViewSystem : ViewPresenter<CraftMono>, IPreInitWait, ISystem
    {
        readonly AtlasService _atlas;

        readonly Settings _def;
        readonly LocalizationService _localization;

        public CraftViewSystem(Settings def)
        {
            _def = def;
            _atlas = W.GetResource<AtlasService>();
            _localization = W.GetResource<LocalizationService>();
            IPreInitWaitRegistry.AddPreInit(this);
        }

        public UniTask PreInitWait(CancellationToken ct)
        {
            return _atlas.LoadAtlas(_def.AtlasKey);
        }

        public void Init()
        {
            base.Init(_def.CraftPrefab, _def.Root, _def.CraftBtn);

            foreach (var entC in WC.Query<All<CraftEntry>>().Entities())
            {
                var item = Object.Instantiate(_def.ItemPrefab);
                var itemDef = entC.Ref<ItemEntry>();

                item.Init(entC, true);
                item.UpdateIcon(_atlas.GetSprite(_def.AtlasKey, itemDef.AKSprite));
                item.UpdateTile(_localization.GetString(itemDef.LKTitle, _def.LocalizationTable));
                item.UpdateDescription(_localization.GetString(itemDef.LKDescription, _def.LocalizationTable));
                View.AddItem(item.transform);
            }
        }

        public void Update()
        {
            if (!IsActive())
                return;

            foreach (var ent in W.Query<All<CraftItemRef, ClickTag>>().Entities())
            {
                ent.Apply<InventoryAdd>(true);
                ent.Apply<ClickTag>(false);
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
            foreach (var ent in W.Query<All<CraftItemRef>>().Entities())
                ent.Apply<ActiveTag>(active);
        }

        [Serializable]
        public class Settings
        {
            public RectTransform Root;
            public Button CraftBtn;
            public PriceItemMono PricePrefab;
            public CraftItemMono ItemPrefab;
            public string AtlasKey = "ItemsAtlas";
            public string LocalizationTable = "ShadowRiftItems";
            public CraftMono CraftPrefab;
        }
    }
}