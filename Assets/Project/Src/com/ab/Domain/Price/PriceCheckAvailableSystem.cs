using com.ab.common;
using com.ab.complexity.core;
using com.ab.domain.craft;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;
using UnityEngine;

namespace com.ab.domain.price
{
    public struct PriceCheckAvailableSystem : IUpdateSystem
    {
        public void Update()
        {
            foreach (var ent in W.Query.Entities<All<PriceRef>, TagAll<ActiveTag>>())
            {
                var item = ent.Ref<PriceRef>().Val;
                var price = ent.GetConfigTable<PriceEntry>().Price;

                bool available = true;

                foreach (var craftAmount in price)
                {
                    if (!craftAmount.Item.TryToFindRuntimeRefByTag<InvTag>(out var invEnt, out _))
                    {
                        available = false;
                        break;
                    }

                    if (invEnt.Ref<Amount>().Val < craftAmount.Amount)
                    {
                        available = false;
                        break;
                    }
                }

                item.UpdateAvailable(available);
            }
        }
    }
}