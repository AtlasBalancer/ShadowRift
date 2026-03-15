using System;
using com.ab.complexity.core;
using com.ab.core;
using UnityEngine;
using UnityEngine.UI;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;
using Project.Src.com.ab.Domain.ItemTable;

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
        }

        Settings _def;
        DropTableService _dropTable;
        public CraftViewSystem(Settings def) => _def = def;

        public void Init()
        {
            _dropTable = W.Context<DropTableService>.Get();

            base.Init(_def.CraftViewPrefab, _def.Root, _def.CraftBtn);

            foreach (var item in View.Items)
            {
                if (!_dropTable.Def.Craft.Entries.TryGetValue(item.ID, out var craftDef))
                    throw new ArgumentException($"{nameof(CraftViewSystem)}:: Can't find {item.ID} in itemTable.Craft");

                // item.Def = craftDef;
                item.Ent = W.Entity.New();

                foreach (var priceDef in craftDef.Price)
                {
                    var priceView = UnityEngine.Object
                        .Instantiate(_def.PricePrefab, item.PriceContainer);

                    priceView.Icon.sprite = priceDef.Icon;
                    // priceView.Amount.SetText(priceDef.Amount.ToString());
                }
            }
        }

        public void Update()
        {
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
        }

        // void DecreaseResourceFromInventory(Price price) => 
            // _inventory.Add(price.Resource, -price.Amount);
    }
}