using System;
using com.ab.common;
using Project.Src.com.ab.Domain.Collect;

namespace com.ab.domain.collect
{
    public class PlacedEntryDef : StaticEntryParamDef<PlacedEntryDef.Settings>,
        IStaticUpdateDef, IStaticCreateProtoEntityDef, IStaticContextSetDef
    {
        public void SetContext()
        {
            // W.Context<ItemTable>.Set(Def.ItemTable);
        }

        public void CreateProtoEntities()
        {
        }

        public void RegisterUpdate()
        {
            Sys.Add(new PlacedToInventorySystem(Def.CollectToInventorySystem));
            Sys.Add(new PlacedSpawnSystem(Def.CollectSpawnSystem));
        }

        [Serializable]
        public class Settings
        {
            // public ItemTable ItemTable;
            public PlacedToInventorySystem.Settings CollectToInventorySystem;
            public PlacedSpawnSystem.Settings CollectSpawnSystem;
        }
    }
}