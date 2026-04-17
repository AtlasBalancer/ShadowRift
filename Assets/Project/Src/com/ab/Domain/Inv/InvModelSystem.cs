using System;
using com.ab.common;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvModelSystem : ISystem
    {
        Settings _def;

        public void Update()
        {
            foreach (var ent in W.Query<All<InvToUpdateTag>>().Entities())
                ent.Apply<InvToUpdateTag>(false);

            foreach (var addEnt in W.Query<All<InventoryAdd>>().Entities())
            {
                if (!addEnt.TryToFindRuntimeRefByTag<InvTag>(out var ent, out var config))
                {
                    ent = W.NewEntity<Default>();
                    ent.Set(config);
                    ent.Set(new Amount(0));
                    ent.Apply<InvTag>(true);
                }

                ent.Ref<Amount>().Increase(addEnt.GetAmount());
                ent.Apply<InvToUpdateTag>(true);

                addEnt.Apply<InventoryAdd>(false);
            }
        }

        [Serializable]
        public class Settings
        {
        }
    }
}