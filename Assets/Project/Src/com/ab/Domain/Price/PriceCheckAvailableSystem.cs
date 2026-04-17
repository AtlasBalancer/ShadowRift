using com.ab.common;
using FFS.Libraries.StaticEcs;
using Project.Src.com.ab.Domain.Inventory;

namespace com.ab.domain.price
{
    public readonly struct PriceCheckAvailableSystem : ISystem
    {
        public void Update()
        {
            foreach (var ent in W.Query<All<PriceRef, ActiveTag>>().Entities())
            {
                var item = ent.Ref<PriceRef>().Val;
                var price = ent.GetConfigTable<PriceEntry>().Price;

                var available = true;

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