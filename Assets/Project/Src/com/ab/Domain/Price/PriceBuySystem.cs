using com.ab.common;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;
using UnityEngine;

namespace com.ab.domain.price
{
    public readonly struct PriceBuySystem : IUpdateSystem
    {
        public void Update()
        {
            foreach (var ent in W.Query.Entities<All<PriceRef>, TagAll<PriceBuyTag>>())
            {
                var price = ent.GetConfigTable<PriceEntry>().Price;

                foreach (var priceItem in price)
                {
                    if (!priceItem.Item.TryToFindRuntimeRefByTag<InvTag>(out var invEnt, out _))
                    {
                        Debug.LogError(
                            $"{nameof(PriceBuySystem)}::{nameof(PriceBuyTag)}:: Can't find {priceItem.Item}, with amount {priceItem.Amount}");
                        break;
                    }

                    ref var amountRef = ref invEnt.Ref<Amount>();

                    if (amountRef.Val < priceItem.Amount)
                    {
                        Debug.LogError(
                            $"{nameof(PriceBuySystem)}::{nameof(PriceBuyTag)}:: Not enough amount. Price: {priceItem.Item}, with amount {priceItem.Amount}, inventory amount {amountRef.Val}");
                        break;
                    }

                    amountRef.Val -= priceItem.Amount;
                }
                
                ent.ApplyTag<PriceBuyTag>(false);
            }
        }
    }

    public readonly struct PriceBuyTag : ITag
    {
    }
}