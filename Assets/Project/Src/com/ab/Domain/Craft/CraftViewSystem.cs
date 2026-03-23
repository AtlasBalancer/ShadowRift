using System;
using System.Threading;
using com.ab.common;
using com.ab.complexity.core;
using com.ab.core;
using com.ab.domain.item;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;

namespace com.ab.domain.craft
{
    public class CraftViewSystem : ViewPresenter<CraftViewMono>, IPreInitLoad, IInitSystem, IUpdateSystem
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
            base.Init(_def.CraftViewPrefab, _def.Root, _def.CraftBtn);

            foreach (var entC in WC.Query.Entities<All<CraftEntry>>())
            {
                var item = UnityEngine.Object.Instantiate(_def.ItemPrefab);
                var itemDef = entC.Ref<ItemEntry>();

                item.Init(entC);
                item.UpdateIcon(_atlas.GetSprite(_def.AtlasKey, itemDef.AKSprite));
                item.UpdateTile(_localization.GetString(itemDef.LKTitle, _def.LocalizationTable));
                item.UpdateDescription(_localization.GetString(itemDef.LKDescription, _def.LocalizationTable));
                View.AddItem(item.transform);
                item.Ent.Add(new CraftItemRef(item));

                var entry = entC.Ref<CraftEntry>();
                
                foreach (var priceDef in entry.Price)
                {
                    var sprite = _atlas.GetSprite(_def.AtlasKey, priceDef.Item.RuntimeID);

                    var priceItem = UnityEngine.Object.Instantiate(_def.PricePrefab);
                    priceItem.UpdateData(sprite, priceDef.Amount);
                    item.AddPrice(priceItem.transform);
                }
            }
        }

        public void Update()
        {
            if (!IsActive())
                return;

            foreach (var ent in W.Query.Entities<All<CraftItemRef>>())
            {
                var @ref = ent.Ref<CraftItemRef>().Val;
                var price = ent.GetConfigTable<CraftEntry>().Price;

                bool craftAvailable = true;

                foreach (var craftAmount in price)
                {
                    if (!craftAmount.Item.TryToFindConfigRefByTag<InvTag>(out var invEnt, out _))
                    {
                        craftAvailable = false;
                        break;
                    }

                    if (invEnt.Ref<Amount>().Val < craftAmount.Amount)
                    {
                        craftAvailable = false;
                        break;
                    }
                }

                @ref.UpdateCraftAvailable(craftAvailable);
            }

            foreach (var ent in W.Query.Entities<All<CraftItemRef>, TagAll<Click>>())
            {
                ent.ApplyTag<InventoryAdd>(true);
            }

            /*

            for (var i = 0; i < View.Items.Length; i++)
            {
                var item = View.Items[i];

                bool available = true;

                // foreach (var price in item.Def.Price)
                // {
                //     bool active = _inventory.HasAndGreaterThan(price.Resource, price.Amount);
                //
                //     if (!active)
                //         available = false;
                // }

                item.Active(available);
            }

            foreach (var ent in W.Query.Entities<All<CraftCommand>>())
            {
                // var def = ent.Ref<CraftCommand>().Def;

                // foreach (var price in def.Price)
                // DecreaseResourceFromInventory(price);

                // W.Events.Send(new InventoryAddItem { ID = def.ItemDefID, Amount = 1 });

                ent.Delete<CraftCommand>();
            }
*/
        }

        // void DecreaseResourceFromInventory(Price price) => 
        // _inventory.Add(price.Resource, -price.Amount);

        [Serializable]
        public class Settings
        {
            public RectTransform Root;
            public Button CraftBtn;
            public CraftViewMono CraftViewPrefab;
            public CraftPriceItemMono PricePrefab;
            public CraftItemMono ItemPrefab;
            public string AtlasKey = "ItemsAtlas";
            public string LocalizationTable = "ShadowRiftItems";
        }
    }
}