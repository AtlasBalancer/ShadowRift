using System;
using com.ab.common;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InvModelSystem : IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
        }

        Settings _def;

        public void Update()
        {
            foreach (var ent in W.Query.Entities<TagAll<InvToUpdateTag>>())
                ent.ApplyTag<InvToUpdateTag>(false);

            foreach (var addEnt in W.Query.Entities<TagAll<InventoryAdd>>())
            {
                if (!addEnt.TryToFindConfigRefByTag<InvTag>(out var ent, out uint id))
                {
                    ent = W.Entity.New();
                    ent.Add(new ConfigRef(id));
                    ent.Add(new Amount(0));
                    ent.ApplyTag<InvTag>(true);
                }
                 
                ent.Ref<Amount>().Increase(addEnt.GetAmount());
                ent.ApplyTag<InvToUpdateTag>(true);
                
                addEnt.ApplyTag<InventoryAdd>(false);
            }
        }
    }
}