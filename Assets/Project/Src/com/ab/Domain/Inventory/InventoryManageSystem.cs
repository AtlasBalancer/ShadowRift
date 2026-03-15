using System;
using com.ab.common;
using com.ab.complexity.core;
using FFS.Libraries.StaticEcs;

namespace Project.Src.com.ab.Domain.Inventory
{
    public class InventoryManageSystem : IUpdateSystem
    {
        [Serializable]
        public class Settings
        {
        }

        Settings _def;

        public void Update()
        {
            foreach (var ent in W.Query.Entities<TagAll<InventoryToUpdate>>())
                ent.ApplyTag<InventoryToUpdate>(false);

            foreach (var addEnt in W.Query.Entities<TagAll<InventoryAdd>>())
            {
                var id = addEnt.Ref<IDRef>().ID;
                
                /*
                if (!id.TryFindByTag<Inventory>(out var ent))
                {
                    ent = W.Entity.New();
                    ent.Add(new IDRef(id));
                    ent.Add(new Amount(0));
                    ent.ApplyTag<Inventory>(true);
                }

                ent.Ref<Amount>().Increase(addEnt.GetAmount());
                ent.ApplyTag<InventoryToUpdate>(true);

                addEnt.ApplyTag<InventoryAdd>(false);
                */
            }
        }
    }
}