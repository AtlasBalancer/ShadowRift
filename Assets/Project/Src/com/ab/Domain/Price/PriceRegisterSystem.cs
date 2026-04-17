using System;
using com.ab.common;
using com.ab.domain.item;
using FFS.Libraries.StaticEcs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.ab.domain.price
{
    public readonly struct PriceRegisterSystem : ISystem
    {
        [Serializable]
        public class Settings
        {
            public PriceItemMono ItemPrefab;
            public string ItemAtlasKey = "ItemsAtlas";
        }

        readonly Settings _def;
        readonly AtlasService _atlas;

        public PriceRegisterSystem(Settings def)
        {
            _def = def;
            _atlas = W.GetResource<AtlasService>();
        }


        public void Update()
        {
            foreach (var ent in W.Query<All<PriceRef, PriceRegisterTag>>().Entities())
            {
                var priceDef = ent.GetConfigTable<PriceEntry>();

                var item = ent.Ref<PriceRef>().Val;
                item.ClearItems();

                foreach (var priceDefItem in priceDef.Price)
                    item.Items.Add(CreteItem(priceDefItem, item.ItemContainer));

                ent.Apply<PriceRegisterTag>(false);
            }
        }

        PriceItemMono CreteItem(PriceAmount def, Transform parent)
        {
            if (!def.Item.GetConfig<ItemEntry>(out var itemEntry, out _))
                throw new ArgumentException($"{nameof(PriceRegisterSystem)}::{nameof(CreteItem)}:: " +
                                            $"Can't find {def.Item.RuntimeID} in WC.{nameof(ItemEntry)}");

            var itemRef = Object.Instantiate(_def.ItemPrefab);
            var sprite = _atlas.GetSprite(_def.ItemAtlasKey, itemEntry.AKSprite);
            itemRef.UpdateData(sprite, def.Amount, parent);

            return itemRef;
        }
    }
}