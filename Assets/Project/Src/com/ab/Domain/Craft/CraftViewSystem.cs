using System;
using com.ab.common;
using com.ab.complexity.core;
using com.ab.core;
using com.ab.domain.placed;
using UnityEngine;
using UnityEngine.UI;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;
using Project.Src.com.ab.Domain.ItemTable;
using Object = System.Object;

namespace com.ab.domain.craft
{
    public class CraftViewSystem : ViewPresenter<CraftViewMono>, IInitSystem, IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
            public RectTransform Root;
            public Button CraftBtn;
            public CraftViewMono CraftViewPrefab;
            public CraftPriceItemMono PricePrefab;
            public CraftItemMono ItemPrefab;
            public string AtlasKey = "ItemsAtlas";
        }

        Settings _def;
        DropTableService _dropTable;
        public CraftViewSystem(Settings def) => _def = def;

        AtlasService _atlas;
        
        public void Init()
        {
            _atlas = W.Context<AtlasService>.Get();
            
            _dropTable = W.Context<DropTableService>.Get();

            base.Init(_def.CraftViewPrefab, _def.Root, _def.CraftBtn);
            _atlas.LoadAtlas(_def.AtlasKey).GetAwaiter().GetResult();

            foreach (var ent in W.Query.Entities<All<CraftEntry>>())
            {
                if (ent.HasAllOf<CraftItemRef>())
                    continue;

                var item = UnityEngine.Object.Instantiate(_def.ItemPrefab);
                item.Init(ent);
                View.AddItem(item.transform);

                var entry = ent.Ref<CraftEntry>();

                foreach (var priceDef in entry.Price)
                {
                    var spriteKey = priceDef.Item.RuntimeID.Ref<ItemEntry>().LKSprite;
                    var sprite = _atlas.GetSprite(_def.AtlasKey, spriteKey);
                    
                    var priceItem = UnityEngine.Object.Instantiate(_def.PricePrefab);
                    item.AddPrice(priceItem.transform);
                    priceItem.UpdateData(sprite, priceDef.Amount);
                }
            }
        }

        public void Update()
        {
            /*
            
            if (!base.IsActive())
                return;

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
    }
}